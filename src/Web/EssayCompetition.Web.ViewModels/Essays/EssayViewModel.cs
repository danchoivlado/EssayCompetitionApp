namespace EssayCompetition.Web.ViewModels.Essays
{
    using System;
    using System.Collections.Generic;

    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;

    public class EssayViewModel : IMapFrom<Essay>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserUserName { get; set; }

        public int ContestId { get; set; }

        public string ContestName { get; set; }

        public virtual IEnumerable<CommentViewModel> Comments { get; set; }

        public string DisplayCreateDate => this.CreatedOn.ToLocalTime().ToShortDateString();
    }
}
