using EssayCompetition.Data.Models;
using EssayCompetition.Services.Mapping;
using System.ComponentModel.DataAnnotations;

namespace EssayCompetition.Web.ViewModels.Contest.Sign
{
    public class IndexViewModel : IMapFrom<ApplicationUser>
    {
        [Required]
        [Display(Name ="Your email")]
        public string Email { get; set; }
    }
}
