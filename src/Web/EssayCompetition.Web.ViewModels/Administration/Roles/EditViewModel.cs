namespace EssayCompetition.Web.ViewModels.Administration.Roles
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;
    using Microsoft.AspNetCore.Identity;

    public class EditViewModel : IMapFrom<ApplicationUser>
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        public string RoleId { get; set; }

        public IEnumerable<RoleDropDownViewModel> AllAvailableRoles { get; set; }

        public IEnumerable<string> RolesNames { get; set; }

        public ICollection<IdentityUserRole<string>> Roles { get; set; }
    }
}
