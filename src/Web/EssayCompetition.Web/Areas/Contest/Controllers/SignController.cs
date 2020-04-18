using EssayCompetition.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EssayCompetition.Web.ViewModels.Contest.Sign;
using System.Threading.Tasks;
using EssayCompetition.Services.Data.ContestServices;
using EssayCompetition.Services.Data.SignServices;
using EssayCompetition.Services.Messaging;
using Microsoft.Extensions.Configuration;
using System.Text.Encodings.Web;

namespace EssayCompetition.Web.Areas.Contest.Controllers
{
    public class SignController : ContestController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ISignService signService;
        private readonly IEmailSender emailSender;
        private readonly IConfiguration configuration;

        public SignController(
            UserManager<ApplicationUser> userManager,
            ISignService signService,
            IEmailSender emailSender,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signService = signService;
            this.emailSender = emailSender;
            this.configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel();
            var user = await this.userManager.GetUserAsync(this.User);
            viewModel.Email = user.Email;

            return this.View(viewModel);
        }

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
            }

            var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                    "/Sign/ConfirmEmail",
                    pageHandler: null,
                    values: new { userId = user.Id, code = code },
                    protocol: Request.Scheme);

            await this.emailSender.SendEmailAsync(
                this.configuration["SendGrid:Email"],
                this.configuration["SendGrid:Name"],
                user.Email,
                "Register",
                $"Register for competion {this.signService.GetContestName(contestId)} Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            this.TempData["FormResult"] = "Succesfully send email";

            return this.RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        public IActionResult ConfirmEmail()
        {
            return this.View();
        }
    }
}
/*
 * var code = await _userManager.GenerateChangeEmailTokenAsync(user, Input.NewEmail);
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmailChange",
                    pageHandler: null,
                    values: new { userId = userId, email = Input.NewEmail, code = code },
                    protocol: Request.Scheme);
                await _emailSender.SendEmailAsync(
                    this.configuration["SendGrid:Email"],
                    "EssayCompetition",
                    Input.NewEmail,
                    "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
 */
