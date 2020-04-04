namespace EssayCompetition.Web.ViewModels.Administration.Roles
{
    using System.Collections.Generic;

    using EssayCompetition.Web.ViewModels.Administration.Roles.Shared;

    public class IndexViewModel
    {
        public IEnumerable<UserViewModel> Users { get; set; }

        public int RoleId { get; set; }

        public IEnumerable<RoleViewModel> Roles { get; set; }

        public PagerViewModel Pager { get; set; }
    }
}
