namespace EssayCompetition.Web.Areas.Teacher.Controllers
{
    using System;
    using System.Threading.Tasks;

    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Data.ImageServices;
    using EssayCompetition.Services.Data.TeacherServices;
    using EssayCompetition.Web.ViewModels.Teacher.Reviews;
    using EssayCompetition.Web.ViewModels.Teacher.Reviews.Shared;
    using Ganss.XSS;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ReviewsController : TeacherController
    {
        private const int PageSize = 5;
        private readonly ITeacherService teacherService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IImageService imageService;
        private readonly HtmlSanitizer htmlSanitizer;

        public ReviewsController(ITeacherService teacherService, UserManager<ApplicationUser> userManager, IImageService imageService)
        {
            this.teacherService = teacherService;
            this.userManager = userManager;
            this.imageService = imageService;
            this.htmlSanitizer = new HtmlSanitizer();
        }

        public IActionResult Index(IndexViewModel viewModel)
        {
            viewModel ??= new IndexViewModel();
            viewModel.Pager ??= new PagerViewModel();
            viewModel.Pager.CurrentPage = viewModel.Pager.CurrentPage <= 0 ? 1 : viewModel.Pager.CurrentPage;

            var userId = this.userManager.GetUserId(this.User);
            viewModel.Essays = this.teacherService.GetTeacherNotReviewedEssaysInRange<EssayViewModel>(
                userId,
                viewModel.Pager.CurrentPage,
                PageSize);
            viewModel.Pager.PagesCount = (int)Math.Ceiling((double)this.teacherService.GetTeacherNotReviewedEssaysCount(userId) / PageSize);

            return this.View(viewModel);
        }

        public IActionResult ReviewEssay(int id)
        {
            var viewModel = new ReviewEssayViewModel();

            if (!this.teacherService.HasEssayWithId(id))
            {
                return this.NotFound();
            }

            viewModel = this.teacherService.GetEssayInfo<ReviewEssayViewModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ReviewEssay(ReviewEssayViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            if (viewModel.ImageContent != null)
            {
                viewModel.ImageUrl = await this.imageService.UploadImageToCloudinaryAsync(viewModel.ImageContent);
            }

            var result = await this.teacherService.UpdateEssayAync(this.GenerateUpdateEssayModel(viewModel));
            if (!result)
            {
                return this.NotFound();
            }

            await this.teacherService.GradeEssayAsync(viewModel.PrivateComments, viewModel.Points, viewModel.Id);

            return this.RedirectToAction("Index");
        }

        private UpdateEssayModel GenerateUpdateEssayModel(ReviewEssayViewModel viewModel)
        {
            UpdateEssayModel updateEssayModel = new UpdateEssayModel()
            {
                Id = viewModel.Id,
                ImageUrl = viewModel.ImageUrl,
                Content = this.htmlSanitizer.Sanitize(viewModel.Content),
                Description = viewModel.Description,
                Title = viewModel.Title,
                UserId = viewModel.UserId,
                ContestId = viewModel.ContestId,
            };

            return updateEssayModel;
        }
    }
}
