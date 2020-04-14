namespace EssayCompetition.Data.Models
{
    using System;
    using System.Collections.Generic;

    using EssayCompetition.Data.Common.Models;

    public class Contest : BaseDeletableModel<int>
    {
        public Contest()
        {
            this.Essays = new HashSet<Essay>();
        }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public ICollection<Essay> Essays { get; set; }
    }
}
