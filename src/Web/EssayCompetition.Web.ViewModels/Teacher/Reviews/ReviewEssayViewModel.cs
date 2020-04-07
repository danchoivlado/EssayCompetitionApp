namespace EssayCompetition.Web.ViewModels.Teacher.Reviews
{
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;

    public class ReviewEssayViewModel : IMapFrom<Essay>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public Category Category { get; set; }
    }
}
