namespace EssayCompetition.Web.Areas.Teacher.Controllers
{
    using EssayCompetition.Common;
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Data.TeacherServices;
    using EssayCompetition.Web.ViewModels.Teacher.Reviews;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class ReviewsController : TeacherController
    {
        private readonly ITeacherService teacherService;
        private readonly UserManager<ApplicationUser> userManager;

        public ReviewsController(ITeacherService teacherService, UserManager<ApplicationUser> userManager)
        {
            this.teacherService = teacherService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();

            var user = this.userManager.GetUserId(this.User);
            viewModel.Essays = this.teacherService.GetTeacherEssays<EssayViewModel>(user);

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
            viewModel.AllAvailableCategories = this.teacherService.GetAllAvilableCategories<CategoryDropDownViewModel>();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ReviewEssay(ReviewEssayViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                viewModel.AllAvailableCategories = this.teacherService.GetAllAvilableCategories<CategoryDropDownViewModel>();
                return this.View(viewModel);
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
                CategoryId = viewModel.CategoryId,
                Content = viewModel.Content,
                Description = viewModel.Description,
                Title = viewModel.Title,
                UserId = viewModel.UserId,
            };

            return updateEssayModel;
        }
    }
}
