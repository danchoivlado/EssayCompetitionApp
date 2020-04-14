namespace EssayCompetition.Web.ViewModels.Administration.Contest.Shared
{
    using EssayCompetition.Services.Mapping;

    public class CategoryDropDownViewModel : IMapFrom<EssayCompetition.Data.Models.Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
