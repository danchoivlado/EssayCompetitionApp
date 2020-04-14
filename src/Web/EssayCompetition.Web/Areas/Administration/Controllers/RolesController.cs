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
        private const int PageSize = 10;
        private readonly IUsersService usersService;
        private readonly IRolesService rolesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public RolesController(
            IUsersService usersService,
            IRolesService rolesService,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
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

            viewModel.Users = this.usersService.GetUsersWithRoles<UserViewModel>(
                viewModel.Pager.CurrentPage,
                PageSize,
                viewModel.SearchString,
                viewModel.SortOrder,
                viewModel.SearchOnlyDeleted);

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

            if (this.rolesService.HasRoleWithName(viewModel.Name))
            {
                this.ModelState.AddModelError("Name", "There is already role with this name.");
                return this.View(viewModel);
            }

            await this.rolesService.CreateRoleAsync(viewModel.ToQueryable());

            return this.RedirectToAction("Index");
        }

        public IActionResult Edit(string id)
        {
            if (!this.usersService.HasUserWithId(id))
            {
                return this.NotFound();
            }

            var viewModel = this.usersService.GetUserById<EditViewModel>(id);

            viewModel.RolesNames = this.usersService.GetUserRolesNames(viewModel.Roles.Select(x => x.RoleId));
            viewModel.AllAvailableRoles = this.rolesService.GetAll<RoleDropDownViewModel>();

            return this.View(viewModel);
        }

        public IActionResult Details(string id, bool fromDeleted)
        {
            if (!this.usersService.HasUserWithId(id) && !this.usersService.HasDeletedUserWithId(id))
            {
                return this.NotFound();
            }

            var viewModel = this.usersService.GetUserById<DetailsViewModel>(id);
            viewModel.FromDeleted = fromDeleted;
            return this.View(viewModel);
        }

        public async Task<IActionResult> DeleteUserRole(string id, string roleName)
        {
            var role = await this.roleManager.FindByNameAsync(roleName);
            var user = await this.userManager.FindByIdAsync(id);
            await this.userManager.RemoveFromRoleAsync(user, role.Name);

            return this.RedirectToAction("Edit", new { @id = id });
        }

        public IActionResult AddRoleToUser(string id)
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToRole(EditViewModel editViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Index");
            }

            if (!this.usersService.HasUserWithId(editViewModel.Id) && !this.rolesService.HasRoleWithId(editViewModel.RoleId))
            {
                return this.NotFound();
            }

            await this.usersService.UpdateUserAsync(editViewModel.Id, editViewModel.UserName, editViewModel.Email);
            var role = await this.roleManager.FindByIdAsync(editViewModel.RoleId);
            var user = await this.userManager.FindByIdAsync(editViewModel.Id);
            await this.userManager.AddToRoleAsync(user, role.Name);
            return this.RedirectToAction("Edit", new { @id = editViewModel.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel editViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(editViewModel);
            }

            if (!this.usersService.HasUserWithId(editViewModel.Id))
            {
                return this.NotFound();
            }

            await this.usersService.UpdateUserAsync(editViewModel.Id, editViewModel.UserName, editViewModel.Email);
            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (!this.usersService.HasUserWithId(id))
            {
                return this.NotFound();
            }

            await this.usersService.DeleteUserAsync(id);
            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> UnDelete(string id)
        {
            if (!this.usersService.HasDeletedUserWithId(id))
            {
                return this.NotFound();
            }

            await this.usersService.UnDeleteUserAsync(id);
            return this.RedirectToAction("Index", new { @SearchOnlyDeleted = true });
        }
    }
}
