namespace EssayCompetition.Web.ViewModels.Contact
{
    using System.ComponentModel.DataAnnotations;

    public class EmailViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
