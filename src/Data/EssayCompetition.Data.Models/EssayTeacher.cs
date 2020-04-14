namespace EssayCompetition.Data.Models
{
    using EssayCompetition.Data.Common.Models;

    public class EssayTeacher : BaseDeletableModel<int>
    {
        public int EssayId { get; set; }

        public virtual Essay Essay { get; set; }

        public string TeacherId { get; set; }

        public virtual ApplicationUser Teacher { get; set; }
    }
}
