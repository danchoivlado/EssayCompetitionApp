namespace EssayCompetition.Web.ViewModels.Administration.Roles
{
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;

    public class DetailsViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public string ConcurrencyStamp { get; set; }

        public string SecurityStamp { get; set; }

        public bool EmailConfirmed { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        public bool FromDeleted { get; set; }
    }
}
