namespace EssayCompetition.Web.ViewModels.Contest.MyEssay
{
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;

    public class GradeViewModel : IMapFrom<Grade>
    {
        public int Points { get; set; }

        public string PrivateComments { get; set; }
    }
}
