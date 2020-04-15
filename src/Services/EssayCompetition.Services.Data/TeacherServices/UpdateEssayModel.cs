namespace EssayCompetition.Services.Data.TeacherServices
{
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Mapping;

    public class UpdateEssayModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public string UserId { get; set; }

        public int ContestId { get; set; }

        public string SanitizedContent => HtmlSanitizer
    }
}
