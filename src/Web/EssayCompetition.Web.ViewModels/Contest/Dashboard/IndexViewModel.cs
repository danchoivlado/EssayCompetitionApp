namespace EssayCompetition.Web.ViewModels.Contest.Dashboard
{
    using System;

    using EssayCompetition.Services.Mapping;

    public class IndexViewModel : IMapFrom<EssayCompetition.Data.Models.Contest>
    {
        public int Id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public bool HasContextNow { get; set; }

        public bool UserRegistredForContext { get; set; }

        public string ContestInfo => this.StartTime != default(DateTime) ?
            $"Next contest is at {this.StartTime.ToLocalTime().ToShortDateString()} at {this.StartTime.ToLocalTime().ToShortTimeString()}"
            : string.Empty;
    }
}
