namespace EssayCompetition.Web.ViewModels.Teacher.Reviewed
{
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;

    public class EssayViewModel : IMapFrom<Essay>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}