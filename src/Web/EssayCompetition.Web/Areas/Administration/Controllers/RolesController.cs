namespace EssayCompetition.Web.Areas.Administration.Controllers
{
    using EssayCompetition.Services.Data.UsersServices;
    using Microsoft.AspNetCore.Mvc;
    using EssayCompetition.Web.ViewModels.Administration.Roles;
    using System.Linq;

    public class RolesController : AdministrationController
    {
        private readonly IUsersService usersService;

        public RolesController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();
            viewModel.Users = this.usersService.GetUsersWithRoles<UserViewModel>();

            foreach (var user in viewModel.Users)
            {
                user.RolesNames = this.usersService.GetUserRolesNames(user.Roles.Select(x => x.RoleId));
            }

            return this.View(viewModel);
        }
    }
}