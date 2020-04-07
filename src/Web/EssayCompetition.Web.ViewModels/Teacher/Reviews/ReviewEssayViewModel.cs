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

        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public Category Category { get; set; }

        public IFormFile ImageContent { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<CategoryDropDownViewModel> AllAvailableCategories { get; set; }

        public ApplicationUser User { get; set; }

        [Display(Name = "Made by user with id")]
        public string UserId => this.User.Id;
    }
}
