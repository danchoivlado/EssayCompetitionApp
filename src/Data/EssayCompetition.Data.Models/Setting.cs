namespace EssayCompetition.Data.Models
{
    using EssayCompetition.Data.Common.Models;

    public class Setting : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
