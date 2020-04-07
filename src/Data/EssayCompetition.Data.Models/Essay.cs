namespace EssayCompetition.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using EssayCompetition.Data.Common.Models;

    public class Essay : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
