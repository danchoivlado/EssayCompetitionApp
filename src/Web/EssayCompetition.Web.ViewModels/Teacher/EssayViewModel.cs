namespace EssayCompetition.Web.ViewModels.Teacher
{
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;

    public class EssayViewModel : IMapFrom<Essay>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }
    }
}
