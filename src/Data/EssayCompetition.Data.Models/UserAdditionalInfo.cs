using EssayCompetition.Data.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace EssayCompetition.Data.Models
{
    public class UserAdditionalInfo : BaseDeletableModel<int>
    {
        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string FullName { get; set; }

        public string ImageUrl { get; set; }

        public string ContactEmail { get; set; }

        public string ConntactPhone { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Social { get; set; }
    }
}
