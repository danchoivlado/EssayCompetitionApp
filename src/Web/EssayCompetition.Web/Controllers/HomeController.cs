namespace EssayCompetition.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using EssayCompetition.Web.ViewModels.Contact;

    using EssayCompetition.Services.Data.CalendarServices;
    using EssayCompetition.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using EssayCompetition.Services.Messaging;
    using SendGrid;
    using SendGrid.Helpers.Mail;
    using Microsoft.Extensions.Configuration;

    public class HomeController : BaseController
    {
        private readonly ICalendarService calendarService;
        private readonly IEmailSender emailSender;
        private readonly IConfiguration configuration;

        public HomeController(ICalendarService calendarService, IEmailSender emailSender, IConfiguration configuration)
        {
            this.calendarService = calendarService;
            this.emailSender = emailSender;
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Calendar(int curentYear, int currentMonth, bool isPlus)
        {
            var dayNow = this.calendarService.GetDate(currentMonth, curentYear, isPlus);
            var viewModel = this.calendarService.GetCalendarInfo(dayNow.Month, dayNow.Year);
            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult About()
        {
            return this.View();
        }

        public IActionResult Contact()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(EmailViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            await this.emailSender.SendEmailAsync("essaycompetitionsender@abv.bg", viewModel.Email, "essaycompetitionsender@abv.bg", viewModel.Subject, viewModel.Message);
            return this.View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
