namespace EssayCompetition.Web.ViewModels.Contest.Dashboard
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using EssayCompetition.Services.Mapping;

    public class ReviewContextDetails : IMapFrom<EssayCompetition.Data.Models.Contest>
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        [Display(Name = "Contest starts at")]
        public DateTime CurrentStartTime => this.StartTime.ToLocalTime();

        [Display(Name = "Contest ends at")]
        public DateTime CurrentEndTime => this.EndTime.ToLocalTime();

        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Category Title")]
        public string CategoryTitle { get; set; }

        [Display(Name = "Category Description")]
        public string CategoryDescription { get; set; }
    }
}
