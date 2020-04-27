namespace EssayCompetition.Web.ViewModels.Administration.Roles
{
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;

    public class RoleViewModel : IMapFrom<ApplicationRole>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
