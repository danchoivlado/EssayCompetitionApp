namespace EssayCompetition.Web.ViewModels.Teacher.Reviewed
{
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;

    public class EssayViewModel : IMapFrom<Essay>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ContestName { get; set; }

        public string ShortDescription => /*this.Description != null ? */
            this.Description.Length <= 50 ? this.Description : this.Description.Substring(0, 50) + "...";//: string.Empty;
    }
}
