namespace EssayCompetition.Web.ViewModels.Teacher.Reviewed
{
    using System.ComponentModel.DataAnnotations;

    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;

    public class GradeViewModel : IMapFrom<Grade>
    {
        [Range(0, 100)]
        [Required]
        public int Points { get; set; }

        [Required]
        public string PrivateComments { get; set; }
    }
}
