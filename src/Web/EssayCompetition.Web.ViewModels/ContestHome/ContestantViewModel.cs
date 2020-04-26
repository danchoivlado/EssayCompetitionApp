namespace EssayCompetition.Web.ViewModels.ContestHome
{
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;

    public class ContestantViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string EssayName { get; set; }

        public int EssayPoints { get; set; }

        public string DisplayGrade => this.EssayPoints == -1 ? "Not gradet" : this.EssayPoints.ToString();
    }
}
