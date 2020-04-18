namespace EssayCompetition.Web.Areas.Contest.Controllers
{
    using System;

    using EssayCompetition.Services.Data.ContestServices;
    using EssayCompetition.Web.ViewModels.Contest.Dashboard;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : ContestController
    {
        private readonly IContestService contestService;

        public DashboardController(IContestService contestService)
        {
            this.contestService = contestService;
        }

        public IActionResult Index()
        {
            var viewModel = this.contestService.NextContext<IndexViewModel>();
            if (viewModel == null)
            {
                return this.View(new IndexViewModel());
            }

            if (viewModel.HasContextNow = this.contestService.HasContextNow(DateTime.Now))
            {
                viewModel.HasContextNow = true;
                viewModel.UserRegistredForContext = true; //TODO
                if (this.TempData["FormResult"] == null)
                {
                    this.TempData["FormResult"] = $"Contest has begun and will end at {viewModel.EndTime.ToLocalTime().ToShortTimeString()}";
                }
            }

            return this.View(viewModel);
        }

        public IActionResult Review(int id)
        {
            if (!this.contestService.HasContestWithId(id))
            {
                return this.NotFound();
            }

            var viewModel = this.contestService.GetContestDetails<ReviewContextDetails>(id);
            return this.View(viewModel);
        }
    }
}
