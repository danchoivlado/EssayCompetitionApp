namespace EssayCompetition.Web.ViewModels.Teacher.Reviewed
{
    using System.Collections.Generic;

    using EssayCompetition.Web.ViewModels.Teacher.Reviewed.Shared;

    public class IndexViewModel
    {
        public IEnumerable<EssayViewModel> Essays { get; set; }

        public PagerViewModel Pager { get; set; }
    }
}
