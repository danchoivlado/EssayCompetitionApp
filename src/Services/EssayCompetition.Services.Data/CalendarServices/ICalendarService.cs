namespace EssayCompetition.Services.Data.CalendarServices
{
    using System;

    using EssayCompetition.Web.ViewModels.Calendars;

    public interface ICalendarService
    {
        public CalendarViewModel GetCalendarInfo(int month, int year);

        public DateTime GetDate(int month, int year, bool isPlus);
    }
}
