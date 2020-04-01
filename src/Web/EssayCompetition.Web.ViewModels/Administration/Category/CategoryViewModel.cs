namespace EssayCompetition.Web.ViewModels.Administration.Category
{
    using EssayCompetition.Services.Mapping;

    public class CategoryViewModel : IMapFrom<EssayCompetition.Data.Models.Category>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
