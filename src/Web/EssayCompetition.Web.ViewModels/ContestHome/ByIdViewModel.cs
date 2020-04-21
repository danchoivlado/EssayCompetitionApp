namespace EssayCompetition.Web.ViewModels.ContestHome
{
    using System.Collections.Generic;

    using EssayCompetition.Web.ViewModels.ContestHome.Shared;

    public class ByIdViewModel
    {
        public IEnumerable<ContestantViewModel> Contestants { get; set; }

        public PagerViewModel Pager { get; set; }
    }
}
