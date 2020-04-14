namespace EssayCompetition.Web.ViewModels.Administration.Contest
{
    using EssayCompetition.Web.ViewModels.Administration.Contest.Shared;
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<ContestViewModel> Contests { get; set; }

        public PagerViewModel Pager { get; set; }
    }
}
