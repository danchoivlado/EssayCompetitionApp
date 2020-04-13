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

        public bool Graded { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int ContestId { get; set; }

        public virtual Contest Contest { get; set; }
    }
}
