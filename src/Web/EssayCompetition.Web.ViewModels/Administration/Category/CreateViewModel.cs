namespace EssayCompetition.Web.ViewModels.Administration.Category
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;


    public class CreateViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public IFormFile Content { get; set; }
    }
}
