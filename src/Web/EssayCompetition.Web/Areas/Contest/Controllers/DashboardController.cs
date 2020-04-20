namespace EssayCompetition.Web.Areas.Contest.Controllers
{
    using System;

    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Data.ContestServices;
    using EssayCompetition.Services.Data.SignServices;
    using EssayCompetition.Web.ViewModels.Contest.Dashboard;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : ContestController
    {
        private readonly IContestService contestService;
        private readonly ISignService signService;
        private readonly UserManager<ApplicationUser> userManager;

        public DashboardController(
            IContestService contestService,
            ISignService signService,
            UserManager<ApplicationUser> userManager)
        {
            this.contestService = contestService;
            this.signService = signService;
            this.userManager = userManager;
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
                var contestId = this.signService.GetNextContestId();
                var userId = this.userManager.GetUserId(this.User);

                if (this.signService.UserAlreadyRegisteredForCompetition(userId, contestId))
                {
                    viewModel.UserRegistredForContext = true;
                }

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
