namespace EssayCompetition.Web.Areas.Contest.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EssayCompetition.Common;
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Data.ContestServices;
    using EssayCompetition.Web.ValidationAttributes;
    using EssayCompetition.Web.ViewModels.Contest.Create;
    using Ganss.XSS;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [HasContest]
    public class CreateController : ContestController
    {
        private const string ContentName = "_EssayContent";
        private const string Title = "_EssayTitle";
        private const string Description = "_EssayDescription";
        private readonly IContestService contestService;
        private readonly HtmlSanitizer htmlSanitizer;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public CreateController(
            IContestService contestService,
            HtmlSanitizer htmlSanitizer,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            this.contestService = contestService;
            this.htmlSanitizer = htmlSanitizer;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var userId = this.userManager.GetUserId(this.User);
            if (this.contestService.IsUserAlreadySubmitedEssay(userId))
            {
                this.TempData["FormResult"] = "You already submitted your essay.";
                return this.RedirectToAction("Index", "Dashboard");
            }

            var viewModel = new IndexViewModel();
            var title = this.HttpContext.Session.GetString(Title);
            var description = this.HttpContext.Session.GetString(Description);
            var content = this.HttpContext.Session.GetString(ContentName);

            viewModel.Title = title;
            viewModel.Description = description;
            viewModel.Content = content;

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(IndexViewModel viewModel, string button)
        {
            if (button == "Save")
            {
                this.PopulateSession(viewModel);
                this.TempData["FormResult"] = "Successfully saved in session.";

                return this.RedirectToAction("Index");
            }

            if (!this.contestService.HasContextNow(DateTime.Now))
            {
                return this.NotFound();
            }

            var userId = this.userManager.GetUserId(this.User);
            var teachers = await this.userManager.GetUsersInRoleAsync(GlobalConstants.TeacherRoleName);

            await this.contestService.SendContestEssayAsync(
               viewModel.Title,
               viewModel.Description,
               this.htmlSanitizer.Sanitize(viewModel.Content),
               userId,
               teachers.Select(x => x.Id));

            this.TempData["FormResult"] = "Successfully send your essay.";
            return this.RedirectToAction("Index", "Dashboard");
        }

        private void PopulateSession(IndexViewModel viewModel)
        {
            if (viewModel.Title != null)
            {
                this.HttpContext.Session.SetString(Title, viewModel.Title);
            }

            if (viewModel.Description != null)
            {
                this.HttpContext.Session.SetString(Description, viewModel.Description);
            }

            if (viewModel.Content != null)
            {
                this.HttpContext.Session.SetString(ContentName, viewModel.Content);
            }
        }
    }
}
