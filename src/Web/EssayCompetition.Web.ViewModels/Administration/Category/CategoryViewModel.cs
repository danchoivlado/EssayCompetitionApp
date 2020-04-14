namespace EssayCompetition.Web.ViewModels.Administration.Category
{
    using EssayCompetition.Services.Mapping;

    public class CategoryViewModel : IMapFrom<EssayCompetition.Data.Models.Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ShortDescription => this.Description.Length > 60 ? this.Description.Substring(0, 60) + "..." : this.Description;

        public string ImageUrl { get; set; }
    }
}
