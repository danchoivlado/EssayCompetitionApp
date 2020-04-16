using EssayCompetition.Web.ViewModels.Calendars;
using System;
using System.Collections.Generic;
using System.Text;

namespace EssayCompetition.Services.Data.CalendarServices
{
    public interface ICalendarService
    {
        public CalendarViewModel GetCalendarInfo(int month, int year);
    }
}
