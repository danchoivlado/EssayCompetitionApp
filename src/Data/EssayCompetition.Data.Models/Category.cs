namespace EssayCompetition.Data.Models
{
    using EssayCompetition.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
