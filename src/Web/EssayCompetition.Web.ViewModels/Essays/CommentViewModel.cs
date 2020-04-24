namespace EssayCompetition.Web.ViewModels.Essays
{
    using System;

    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;

    public class CommentViewModel : IMapFrom<Comment>
    {
        public DateTime CreatedOn { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public string UserUserName { get; set; }

        public string UserImage { get; set; }
    }
}
