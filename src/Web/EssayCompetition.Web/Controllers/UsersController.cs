﻿namespace EssayCompetition.Web.Controllers
{
    using EssayCompetition.Services.Data.UserAdditionalInfoServices;
    using EssayCompetition.Services.Data.UsersServices;
    using Microsoft.AspNetCore.Mvc;
    using EssayCompetition.Web.ViewModels.Identity;

    public class UsersController : BaseController
    {
        private readonly IUserAdditionalInfoService userAdditionalInfoService;
        private readonly IUsersService usersService;

        public UsersController(IUserAdditionalInfoService userAdditionalInfoService, IUsersService usersService)
        {
            this.userAdditionalInfoService = userAdditionalInfoService;
            this.usersService = usersService;
        }

        public IActionResult ById(string id)
        {
            if (!this.usersService.HasUserWithId(id))
            {
                return this.NotFound();
            }

            var viewModel = this.userAdditionalInfoService.GetUserWithIdAdditionalInfo<IndexViewModel>(id);
            return this.View(viewModel);
        }
    }
}
