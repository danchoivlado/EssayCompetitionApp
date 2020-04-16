﻿namespace EssayCompetition.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using EssayCompetition.Services.Data.CalendarServices;
    using EssayCompetition.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ICalendarService calendarService;

        public HomeController(ICalendarService calendarService)
        {
            this.calendarService = calendarService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Calendar()
        {
            var dayNow = DateTime.Now;
            var viewModel = this.calendarService.GetCalendarInfo(dayNow.Month, dayNow.Year);
            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
