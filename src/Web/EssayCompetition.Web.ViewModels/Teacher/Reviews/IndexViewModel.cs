namespace EssayCompetition.Web.ViewModels.Teacher.Reviews
{
    using System.Collections.Generic;

    using EssayCompetition.Web.ViewModels.Teacher.Reviews.Shared;

    public class IndexViewModel
    {
        public IEnumerable<EssayViewModel> Essays { get; set; }

        public PagerViewModel Pager { get; set; }
    }
}
