namespace EssayCompetition.Web.ViewModels.Administration.Contest
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EssayCompetition.Services.Mapping;
    using EssayCompetition.Web.ViewModels.Administration.Contest.Shared;

    public class EditViewModel : IMapFrom<EssayCompetition.Data.Models.Contest>, IValidatableObject
    {
        private const string DateErrorMessage = "End is greater or equal than start.";
        private const string DurationMessage = "Duration of contest should be greater than 30 minutes.";

        public int Id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<CategoryDropDownViewModel> AllAvilableCategory { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.StartTime >= this.EndTime)
            {
                yield return new ValidationResult(DateErrorMessage);
            }

            if ((this.StartTime - this.EndTime).Duration().Minutes < 30)
            {
                yield return new ValidationResult(DurationMessage);
            }
        }
    }
}
