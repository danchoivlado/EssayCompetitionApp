using EssayCompetition.Services.Mapping;
using EssayCompetition.Web.ViewModels.Administration.Contest.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace EssayCompetition.Web.ViewModels.Administration.Contest
{
    public class EditViewModel : IMapFrom<EssayCompetition.Data.Models.Contest>
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<CategoryDropDownViewModel> AllAvilableCategory { get; set; }
    }
}
