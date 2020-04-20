namespace EssayCompetition.Web.ViewModels.Identity
{
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class IndexViewModel : IMapFrom<UserAdditionalInfo>
    {
        public string FullName { get; set; }

        public string ImageUrl { get; set; }

        [EmailAddress]
        public string ContactEmail { get; set; }

        [Phone]
        public string ConntactPhone { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Social { get; set; }

        public string PreviewImage => string.IsNullOrEmpty(this.ImageUrl) ? string.Empty : this.ImageUrl;
    }
}
