namespace EssayCompetition.Web.ViewModels.Teacher.Reviews
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class ReviewEssayViewModel : IMapFrom<Essay>
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string PrivateComments { get; set; }

        [Range(0, 100)]
        [Required]
        public int Points { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        //public Category Category { get; set; }

        public IFormFile ImageContent { get; set; }

        //[Required]
        //public int CategoryId { get; set; }

        //public IEnumerable<CategoryDropDownViewModel> AllAvailableCategories { get; set; }

        [Display(Name = "Made by user with id")]
        public string UserId { get; set; }

        public string TeacherId { get; set; }

        public int ContestId { get; set; }

        [Display(Name = "From contest")]
        public string ContestName { get; set; }
    }
}
