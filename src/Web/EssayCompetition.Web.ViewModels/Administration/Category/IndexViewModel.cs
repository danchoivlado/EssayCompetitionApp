namespace EssayCompetition.Web.ViewModels.Administration.Category
{
    using System.Collections.Generic;

    using EssayCompetition.Web.ViewModels.Administration.Category.Shared;

    public class IndexViewModel
    {
        public PagerViewModel Pager { get; set; }

        public IEnumerable<CategoryViewModel> AllCategories { get; set; }
    }
}
