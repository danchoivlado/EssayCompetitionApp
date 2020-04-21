using EssayCompetition.Services.Data.ContestServices;
using EssayCompetition.Services.Data.UsersServices;
using Microsoft.AspNetCore.Mvc;
using EssayCompetition.Web.ViewModels.ContestHome;

namespace EssayCompetition.Web.Controllers
{
    public class ContestController : BaseController
    {
        private readonly IContestService contestService;
        private readonly IUsersService usersService;

        public ContestController(IContestService contestService, IUsersService usersService)
        {
            this.contestService = contestService;
            this.usersService = usersService;
        }

        public IActionResult ById(int id)
        {
            var viewModel = new ByIdViewModel();

            var contestantIds = this.contestService.GetContestParticipantsIds(id);
            viewModel.Contestants = this.usersService.GetUsersWithIds<ContestantViewModel>(contestantIds);

            foreach (var contestant in viewModel.Contestants)
            {

            }


            return this.View();
        }
    }
}
