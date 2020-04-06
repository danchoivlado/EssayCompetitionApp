namespace EssayCompetition.Web.ViewModels.Administration.Category
{
    using System.ComponentModel.DataAnnotations;

    using EssayCompetition.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class EditViewModel : IMapFrom<EssayCompetition.Data.Models.Category>
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "URL or file path")]
        public string ImageUrl { get; set; }

        public IFormFile Content { get; set; }
    }
}
