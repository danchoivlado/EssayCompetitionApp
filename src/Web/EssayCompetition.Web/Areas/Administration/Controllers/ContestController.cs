namespace EssayCompetition.Web.Areas.Administration.Controllers
{
    using System;

    using EssayCompetition.Services.Data.ContestServices;
    using EssayCompetition.Web.ViewModels.Administration.Contest;
    using EssayCompetition.Web.ViewModels.Administration.Contest.Shared;
    using Microsoft.AspNetCore.Mvc;

    public class ContestController : AdministrationController
    {
        public const int PageSize = 5;
        private readonly IContestService contestService;

        public ContestController(IContestService contestService)
        {
            this.contestService = contestService;
        }

        public IActionResult Index(IndexViewModel viewModel)
        {
            viewModel ??= new IndexViewModel();
            viewModel.Pager = new PagerViewModel();
            viewModel.Pager.CurrentPage = viewModel.Pager.CurrentPage <= 0 ? 1 : viewModel.Pager.CurrentPage;

            viewModel.Contests = this.contestService.GetAllContestsRange<ContestViewModel>(viewModel.Pager.CurrentPage, PageSize);
            viewModel.Pager.PagesCount = (int)Math.Ceiling((double) this.contestService.GetContestsCount() / PageSize);

            return this.View(viewModel);
        }
    }
}