namespace EssayCompetition.Web.Controllers
{
    using System;

    using EssayCompetition.Services.Data.ContestServices;
    using EssayCompetition.Services.Data.EssayServices;
    using EssayCompetition.Services.Data.GradeServices;
    using EssayCompetition.Web.ViewModels.Essays;
    using EssayCompetition.Web.ViewModels.Essays.Shared;
    using Microsoft.AspNetCore.Mvc;

    public class EssayController : Controller
    {
        private readonly IGradeService gradeService;
        private readonly IContestService contestService;
        private readonly IEssayService essayService;
        private const int PageSize = 5;


        public EssayController(
            IGradeService gradeService,
            IContestService contestService,
            IEssayService essayService)
        {
            this.gradeService = gradeService;
            this.contestService = contestService;
            this.essayService = essayService;
        }

        public IActionResult Index(IndexViewModel viewModel)
        {
            viewModel ??= new IndexViewModel();
            viewModel.Pager ??= new PagerViewModel();
            viewModel.Pager.CurrentPage = viewModel.Pager.CurrentPage <= 0 ? 1 : viewModel.Pager.CurrentPage;

            viewModel.Essays = this.essayService.GetEssaysInRange<EssayViewModel>(viewModel.Pager.CurrentPage, PageSize);

            viewModel.Pager.PagesCount = (int)Math.Ceiling((double)this.essayService.GetEssaysCount() / PageSize);

            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var viewModel = this.essayService.GetEssayDetails<EssayViewModel>(id);
            return this.View(viewModel);

        }
    }
}
