namespace EssayCompetition.Web.ViewModels.Contest.MyEssay
{
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;

    public class EssayViewModel : IMapFrom<Essay>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ContestName { get; set; }

        public bool Graded { get; set; }

        public string GradedNormalized => this.Graded ? "Yes" : "No";
    }
}
