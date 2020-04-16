using System;
using System.Collections.Generic;
using System.Text;

namespace EssayCompetition.Web.ViewModels.Calendars
{
    public class CalendarViewModel
    {
        public IEnumerable<CalendarEventViewModel> Events { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }
    }
}
