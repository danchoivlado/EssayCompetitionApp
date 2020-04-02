namespace EssayCompetition.Web.ViewModels.Administration.Roles
{
    using System.Collections;
    using System.Collections.Generic;

    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;
    using Microsoft.AspNetCore.Identity;

    public class UserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public IEnumerable<string> RolesNames { get; set; }

        public string OneLineRoleNames => string.Join(",", this.RolesNames);

        public ICollection<IdentityUserRole<string>> Roles { get; set; }
    }
}
