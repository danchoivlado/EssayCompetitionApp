namespace EssayCompetition.Web.ViewModels.Contest.MyEssay
{
    using System.ComponentModel.DataAnnotations;

    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;

    public class DetailsViewModel : IMapFrom<Essay>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public GradeViewModel Grade { get; set; }

        public string ImageUrl { get; set; }

        [Display(Name = "Made by user with id")]
        public string UserId { get; set; }

        [Display(Name = "From contest")]
        public string ContestName { get; set; }

        public int ContestId { get; set; }
    }
}
