namespace EssayCompetition.Web.Areas.Contest.Controllers
{
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Data.ContestServices;
    using EssayCompetition.Services.Data.SignServices;
    using EssayCompetition.Services.Messaging;
    using EssayCompetition.Web.ViewModels.Contest.Sign;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using PaulMiami.AspNetCore.Mvc.Recaptcha;

    public class SignController : ContestController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ISignService signService;
        private readonly IEmailSender emailSender;
        private readonly IConfiguration configuration;
        private readonly IContestService contestService;

        public SignController(
            UserManager<ApplicationUser> userManager,
            ISignService signService,
            IEmailSender emailSender,
            IConfiguration configuration,
            IContestService contestService)
        {
            this.userManager = userManager;
            this.signService = signService;
            this.emailSender = emailSender;
            this.configuration = configuration;
            this.contestService = contestService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel();
            var user = await this.userManager.GetUserAsync(this.User);
            viewModel.Email = user.Email;

            return this.View(viewModel);
        }

        [ValidateRecaptcha]
        [HttpPost]
        public async Task<IActionResult> Index(IndexViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            var contestId = this.signService.GetNextContestId();
            if (contestId == -1)
            {
                this.TempData["FormResult"] = "Not arranged next contest";
                return this.View(viewModel);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            if (this.signService.UserAlreadyRegisteredForCompetition(user.Id, contestId))
            {
                this.TempData["FormResult"] = "You are already registered";
                return this.View(viewModel);
            }

            var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = this.Url.Action(
                    "ConfirmEmail",
                    "Sign",
                    values: new { userId = user.Id, code = code, contestId = contestId },
                    protocol: this.Request.Scheme);

            await this.emailSender.SendEmailAsync(
                this.configuration["SendGrid:Email"],
                this.configuration["SendGrid:Name"],
                user.Email,
                "Register",
                $"Register for competion {this.signService.GetContestName(contestId)} Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            this.TempData["FormResult"] = "Succesfully send email";

            return this.RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code, int contestId)
        {
            if (!this.contestService.HasContestWithId(contestId))
            {
                return this.View("Error");
            }

            var user = await this.userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return this.View("Error");
            }

            var result = await this.userManager.ConfirmEmailAsync(user, code);
            if (!result.Succeeded)
            {
                return this.View("Error");
            }

            await this.signService.RegisterForContestAsync(userId, contestId);
            this.TempData["FormResult"] = "Succesfully registered for contest";
            return this.RedirectToAction("Index");
        }
    }
}

