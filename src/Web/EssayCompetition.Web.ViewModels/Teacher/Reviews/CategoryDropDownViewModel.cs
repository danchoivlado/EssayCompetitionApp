namespace EssayCompetition.Web.ViewModels.Teacher.Reviews
{
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;

    public class CategoryDropDownViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
