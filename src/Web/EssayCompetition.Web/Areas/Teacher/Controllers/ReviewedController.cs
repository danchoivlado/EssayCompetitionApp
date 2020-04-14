namespace EssayCompetition.Web.Areas.Teacher.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EssayCompetition.Common;
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Data.ImageServices;
    using EssayCompetition.Services.Data.TeacherReviewedServices;
    using EssayCompetition.Services.Data.TeacherServices;
    using EssayCompetition.Web.ViewModels.Teacher.Reviewed;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ReviewedController : TeacherController
    {
        private readonly ITeacherReviewedService teacherReviewedService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IImageService imageService;

        public ReviewedController(ITeacherReviewedService teacherReviewedService, UserManager<ApplicationUser> userManager, IImageService imageService)
        {
            this.teacherReviewedService = teacherReviewedService;
            this.userManager = userManager;
            this.imageService = imageService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();
            var userId = this.userManager.GetUserId(this.User);
            viewModel.Essays = this.teacherReviewedService.GetAllReviewedEssayFromTecher<EssayViewModel>(userId);

            return this.View(viewModel);
        }

        public IActionResult ReviewedEssay(int id)
        {
            var viewModel = new ReviewedEssayViewModel();

            if (!this.teacherReviewedService.HasEssayWithId(id))
            {
                return this.NotFound();
            }

            viewModel = this.teacherReviewedService.GetEssayInfo<ReviewedEssayViewModel>(id);
            viewModel.Grade = this.teacherReviewedService.GetGradeViewModel<GradeViewModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ReviewedEssay(ReviewedEssayViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            if (viewModel.ImageContent != null)
            {
                viewModel.ImageUrl = await this.imageService.UploadImageToCloudinaryAsync(viewModel.ImageContent);
            }

            var result = await this.teacherReviewedService.UpdateEssayAync(this.GenerateUpdateEssayModel(viewModel));
            if (!result)
            {
                return this.NotFound();
            }

            await this.teacherReviewedService.GradeEssayAsync(viewModel.Grade.PrivateComments, viewModel.Grade.Points, viewModel.Id);

            return this.RedirectToAction("Index");
        }

        private UpdateEssayModel GenerateUpdateEssayModel(ReviewedEssayViewModel viewModel)
        {
            UpdateEssayModel updateEssayModel = new UpdateEssayModel()
            {
                Id = viewModel.Id,
                ImageUrl = viewModel.ImageUrl,
                Content = viewModel.Content,
                Description = viewModel.Description,
                Title = viewModel.Title,
                UserId = viewModel.UserId,
                ContestId = viewModel.ContestId,
            };

            return updateEssayModel;
        }
    }
}
