namespace EssayCompetition.Web.Areas.Teacher.Controllers
{
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Data.TeacherServices;
    using EssayCompetition.Web.ViewModels.Teacher.Reviews;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Grade(ReviewEssayViewModel viewModel)
        {
            return this.View();
        }
    }
}
