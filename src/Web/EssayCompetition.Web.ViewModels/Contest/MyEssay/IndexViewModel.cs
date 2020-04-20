namespace EssayCompetition.Web.ViewModels.Contest.MyEssay
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<EssayViewModel> Essays { get; set; }

        public PagerViewModel Pager { get; set; }
    }
}
