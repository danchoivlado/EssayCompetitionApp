namespace EssayCompetition.Web.ViewModels.Administration.Contest
{
    using System;

    using EssayCompetition.Services.Mapping;

    public class ContestViewModel : IMapFrom<EssayCompetition.Data.Models.Contest>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string ShortenDesc =>
            this.Description.Length > 60 ? this.Description.Substring(0, 60) + "..." : this.Description;
    }
}
