namespace EssayCompetition.Web.ViewModels.Administration.Contest
{
    using System.Collections.Generic;

    using EssayCompetition.Web.ViewModels.Administration.Contest.Shared;

    public class IndexViewModel
    {
        public IEnumerable<ContestViewModel> Contests { get; set; }

        public PagerViewModel Pager { get; set; }
    }
}
