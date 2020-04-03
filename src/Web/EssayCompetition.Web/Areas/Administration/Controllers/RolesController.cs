namespace EssayCompetition.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EssayCompetition.Common;
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Data.RolesServices;
    using EssayCompetition.Services.Data.UsersServices;
    using EssayCompetition.Web.ViewModels.Administration.Roles;
    using EssayCompetition.Web.ViewModels.Administration.Roles.Shared;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class RolesController : AdministrationController
    {
        private const int PageSize = 5;
        private readonly IUsersService usersService;
        private readonly IRolesService rolesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public RolesController(IUsersService usersService, IRolesService rolesService, 
            UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.usersService = usersService;
            this.rolesService = rolesService;
            this.userManager = userManager;
            this.roleManager = roleManager;
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

        public IActionResult Create()
        {
            var viewModel = new CreateViewModel();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            await this.rolesService.CreateRoleAsync(viewModel.ToQueryable());

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (!this.usersService.HasUserWithId(id))
            {
                return this.RedirectToAction("Index");
            }

            var viewModel = this.usersService.GetUserById<EditViewModel>(id);

            viewModel.RolesNames = this.usersService.GetUserRolesNames(viewModel.Roles.Select(x => x.RoleId));

            return this.View(viewModel);
        }

        public IActionResult Details(string id)
        {
            if (!this.usersService.HasUserWithId(id))
            {
                return this.RedirectToAction("Index");
            }

            var viewModel = this.usersService.GetUserById<DetailsViewModel>(id);

            return this.View(viewModel);
        }

        public async Task<IActionResult> DeleteUserRole(string id, string roleName)
        {
            var role = await this.roleManager.FindByNameAsync(roleName);
            var user = await this.userManager.FindByIdAsync(id);
            await this.userManager.RemoveFromRoleAsync(user, role.Name);

            return this.RedirectToAction("Edit", new { @id = id });
        }
    }
}
