using EssayCompetition.Data.Common.Models;

namespace EssayCompetition.Data.Models
{
    public class ContestantContest : BaseDeletableModel<int>
    {
        public string ContestantId { get; set; }

        public virtual ApplicationUser Contestant { get; set; }

        public int ContestId { get; set; }

        public virtual Contest Contest { get; set; }
    }
}
