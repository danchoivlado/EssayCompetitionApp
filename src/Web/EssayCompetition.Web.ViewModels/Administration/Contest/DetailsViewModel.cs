namespace EssayCompetition.Web.ViewModels.Administration.Contest
{
    using System;
    using System.Collections.Generic;

    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;

    public class DetailsViewModel : IMapFrom<EssayCompetition.Data.Models.Contest>
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public TimeSpan Duration => (this.StartTime - this.EndTime).Duration();

        public string Name { get; set; }

        public string Description { get; set; }

        public string CategoryTitle { get; set; }

        public string CategoryDescription { get; set; }

        public ICollection<Essay> Essays { get; set; }
    }
}
