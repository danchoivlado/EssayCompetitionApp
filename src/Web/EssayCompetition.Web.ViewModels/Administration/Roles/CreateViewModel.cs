namespace EssayCompetition.Web.ViewModels.Administration.Roles
{
    using System.ComponentModel.DataAnnotations;

    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;

    public class CreateViewModel : IMapTo<ApplicationRole>
    {
        [Required]
        public string Name { get; set; }

        public string NormalizedName => this.Name.ToUpper();
    }
}
