namespace EssayCompetition.Data.Models
{
    using EssayCompetition.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        public int EssayId { get; set; }

        public virtual Essay Essay { get; set; }

        public int? ParentId { get; set; }

        public virtual Comment Parent { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
