namespace EssayCompetition.Services.Data.CalendarServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EssayCompetition.Data.Common.Repositories;
    using EssayCompetition.Data.Models;
    using EssayCompetition.Web.ViewModels.Calendars;

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

        public DateTime GetDate(int month, int year, bool isPlus)
        {
            if (month == default(int) && year == default(int))
            {
                return DateTime.Now;
            }

            if (!isPlus)
            {
                if (month - 1 == 0)
                {
                    month = 12;
                    year -= 1;
                }
                else
                {
                    month -= 1;
                }

                return new DateTime(year, month, 13);
            }
            else
            {
                if (month + 1 == 13)
                {
                    month = 1;
                    year += 1;
                }
                else
                {
                    month += 1;
                }

                return new DateTime(year, month, 13);
            }
        }
    }
}
