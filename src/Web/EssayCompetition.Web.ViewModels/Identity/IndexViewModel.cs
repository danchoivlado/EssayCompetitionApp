namespace EssayCompetition.Web.ViewModels.Identity
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;

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

        public int UserEssaysCount { get; set; }

        public string PreviewImage => string.IsNullOrEmpty(this.ImageUrl) ? string.Empty : this.ImageUrl;

        public string FirstName => string.IsNullOrEmpty(this.FullName) ? this.FullName : this.FullName.Split().First();
    }
}
