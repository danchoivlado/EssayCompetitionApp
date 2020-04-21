namespace EssayCompetition.Web.Areas.Identity.Pages.Account.Manage
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Data.ImageServices;
    using EssayCompetition.Services.Data.UserAdditionalInfoServices;
    using EssayCompetition.Web.ViewModels.Identity;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserAdditionalInfoService userAdditionalInfoService;
        private readonly IImageService imageService;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUserAdditionalInfoService userAdditionalInfoService,
            IImageService imageService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.userAdditionalInfoService = userAdditionalInfoService;
            this.imageService = imageService;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            public string UserId { get; set; }

            public IndexViewModel AdditionalInfo { get; set; }

            public IFormFile ImageContent { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await this._userManager.GetUserNameAsync(user);
            var phoneNumber = await this._userManager.GetPhoneNumberAsync(user);
            var userId = this._userManager.GetUserId(this.User);

            this.Username = userName;

            this.Input = new InputModel
            {
                UserId = userId,
                PhoneNumber = phoneNumber
            };

            if (this.userAdditionalInfoService.HasUserAdditionalInfoWithId(userId))
            {
                this.Input.AdditionalInfo = this.userAdditionalInfoService.GetUserWithIdAdditionalInfo<IndexViewModel>(userId);
            }
            else
            {
                this.Input.AdditionalInfo = new IndexViewModel();
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            if (this.Input.ImageContent != null)
            {
                this.Input.AdditionalInfo.ImageUrl = await this.imageService.UploadImageToCloudinaryAsync(this.Input.ImageContent);
            }

            await this.userAdditionalInfoService.UpdateUserAdditionalInfoAsync(
                user.Id,
                this.Input.AdditionalInfo.FullName,
                this.Input.AdditionalInfo.ImageUrl,
                this.Input.AdditionalInfo.ConntactPhone,
                this.Input.AdditionalInfo.ContactEmail,
                this.Input.AdditionalInfo.Country,
                this.Input.AdditionalInfo.City,
                this.Input.AdditionalInfo.Social);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
