﻿namespace EssayCompetition.Web.ViewModels.Administration.Contest
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EssayCompetition.Common;
    using EssayCompetition.Services.Mapping;
    using EssayCompetition.Web.ViewModels.Administration.Contest.Shared;

    public class EditViewModel : IMapFrom<EssayCompetition.Data.Models.Contest>, IValidatableObject
    {

        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryDropDownViewModel> AllAvilableCategory { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.StartTime.Date > this.EndTime.Date)
            {
                yield return new ValidationResult(GlobalConstants.DateErrorMessage);
            }

            if (this.StartTime.TimeOfDay > this.EndTime.TimeOfDay)
            {
                yield return new ValidationResult(GlobalConstants.DateErrorMessage);
            }

            if ((this.EndTime - this.StartTime).Duration().TotalMinutes < 30)
            {
                yield return new ValidationResult(GlobalConstants.DurationMessage);
            }
        }
    }
}
