namespace EssayCompetition.Web.ViewModels.Users
{
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;
    using System;

    public class EssayViewModel : IMapFrom<Essay>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserUserName { get; set; }

        public string DisplayCreateDate => this.CreatedOn.ToLocalTime().ToShortDateString();
    }
}
