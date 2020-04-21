using EssayCompetition.Data.Models;
using EssayCompetition.Services.Mapping;

namespace EssayCompetition.Web.ViewModels.ContestHome
{
    public class ContestantViewModel : IMapFrom<ApplicationUser>
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string EssayName { get; set; }

        public int EssayPoints { get; set; }
    }
}
