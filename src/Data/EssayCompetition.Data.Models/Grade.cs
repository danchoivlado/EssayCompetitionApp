﻿namespace EssayCompetition.Data.Models
{
    using EssayCompetition.Data.Common.Models;

    public class Grade : BaseModel<int>
    {
        public int Points { get; set; }

        public string PrivateComments { get; set; }

        public int EssayId { get; set; }

        public virtual Essay Essay { get; set; }
    }
}
