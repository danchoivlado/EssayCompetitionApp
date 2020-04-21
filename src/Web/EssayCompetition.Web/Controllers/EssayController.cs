namespace EssayCompetition.Web.Controllers
{
    using EssayCompetition.Services.Data.ContestServices;
    using EssayCompetition.Services.Data.EssayServices;
    using EssayCompetition.Services.Data.GradeServices;
    using EssayCompetition.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Mvc;

    public class EssayController : Controller
    {
        private readonly IGradeService gradeService;
        private readonly IContestService contestService;
        private readonly IEssayService essayService;

        public EssayController(
            IGradeService gradeService,
            IContestService contestService,
            IEssayService essayService)
        {
            this.gradeService = gradeService;
            this.contestService = contestService;
            this.essayService = essayService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();

            var lastcontestId = this.contestService.GetLastContestId();
            var essaysIdsOrderedByPoints = this.gradeService.GetEssaysIdsOrderedByPoints();
            viewModel.Essays = this.essayService.GetBestEssaysFromLastContest<EssayViewModel>(lastcontestId, essaysIdsOrderedByPoints);

            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var viewModel = this.essayService.GetEssayDetails<EssayViewModel>(id);
            return this.View(viewModel);

        }
    }
}
