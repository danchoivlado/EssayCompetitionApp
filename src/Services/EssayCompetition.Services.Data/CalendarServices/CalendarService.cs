using EssayCompetition.Data.Common.Repositories;
using EssayCompetition.Data.Models;
using EssayCompetition.Web.ViewModels.Calendars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EssayCompetition.Services.Data.CalendarServices
{
    public class CalendarService : ICalendarService
    {
        private const string CalendarType = "info";
        private readonly IDeletableEntityRepository<Contest> contestRepository;

        public CalendarService(IDeletableEntityRepository<Contest> contestRepository)
        {
            this.contestRepository = contestRepository;
        }

        public CalendarViewModel GetCalendarInfo(int month, int year)
        {
            var viewModel = new CalendarViewModel();
            var eventLis = new List<CalendarEventViewModel>();
            viewModel.Month = month;
            viewModel.Year = year;

            var curMonthEvents = this.contestRepository.All().Where(x => x.StartTime.Month == month);

            foreach (var contest in curMonthEvents)
            {
                var curEvent = new CalendarEventViewModel()
                {
                    Date = contest.StartTime.Date,
                    Title = contest.Name,
                    Type = CalendarType,
                };

                eventLis.Add(curEvent);
            }

            viewModel.Events = eventLis;
            return viewModel;
        }
    }
}
