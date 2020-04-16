namespace EssayCompetition.Web.ViewModels.Contest.Create
{
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;
    using System;

    public class IndexViewModel : IMapTo<Essay>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }
    }
}
