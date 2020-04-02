namespace EssayCompetition.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;

    using EssayCompetition.Services.Data.UsersServices;
    using EssayCompetition.Web.ViewModels.Administration.Roles;
    using EssayCompetition.Web.ViewModels.Administration.Roles.Shared;
    using Microsoft.AspNetCore.Mvc;

    public class RolesController : AdministrationController
    {
        private const int PageSize = 5;
        private readonly IUsersService usersService;

        public RolesController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult Index(IndexViewModel viewModel)
        {
            viewModel ??= new IndexViewModel();
            viewModel.Pager ??= new PagerViewModel();
            viewModel.Pager.CurrentPage = viewModel.Pager.CurrentPage <= 0 ? 1 : viewModel.Pager.CurrentPage;


            viewModel.Users = this.usersService.GetUsersWithRoles<UserViewModel>(viewModel.Pager.CurrentPage, PageSize);

            foreach (var user in viewModel.Users)
            {
                user.RolesNames = this.usersService.GetUserRolesNames(user.Roles.Select(x => x.RoleId));
            }

            viewModel.Pager.PagesCount = (int)Math.Ceiling(this.usersService.GetUsersCount() / (double)PageSize);
            return this.View(viewModel);
        }
    }
}