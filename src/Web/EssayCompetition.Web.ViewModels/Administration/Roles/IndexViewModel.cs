namespace EssayCompetition.Web.ViewModels.Administration.Roles
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<UserViewModel> Users { get; set; }

        public int RoleId { get; set; }

        public IEnumerable<RolesDropDownViewModel> Roles { get; set; }
    }
}
