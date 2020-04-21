namespace EssayCompetition.Web.ViewModels.Essays
{
    using System.Collections.Generic;

    using EssayCompetition.Web.ViewModels.Essays.Shared;

    public class IndexViewModel
    {
        public IEnumerable<EssayViewModel> Essays { get; set; }

        public PagerViewModel Pager { get; set; }
    }
}
