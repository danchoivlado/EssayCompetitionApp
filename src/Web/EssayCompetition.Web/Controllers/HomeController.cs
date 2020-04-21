namespace EssayCompetition.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using EssayCompetition.Services.Data.CalendarServices;
    using EssayCompetition.Services.Data.ContestServices;
    using EssayCompetition.Services.Data.EssayServices;
    using EssayCompetition.Services.Data.GradeServices;
    using EssayCompetition.Services.Messaging;
    using EssayCompetition.Web.ViewModels;
    using EssayCompetition.Web.ViewModels.Contact;
    using EssayCompetition.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using PaulMiami.AspNetCore.Mvc.Recaptcha;
    using SendGrid;
    using SendGrid.Helpers.Mail;

    public class HomeController : BaseController
    {
        private const string FormResult = "You succesfully send email.";
        private readonly ICalendarService calendarService;
        private readonly IEmailSender emailSender;
        private readonly IConfiguration configuration;
        private readonly IGradeService gradeService;
        private readonly IContestService contestService;
        private readonly IEssayService essayService;

        public HomeController(
            ICalendarService calendarService,
            IEmailSender emailSender,
            IConfiguration configuration,
            IGradeService gradeService,
            IContestService contestService,
            IEssayService essayService)
        {
            this.calendarService = calendarService;
            this.emailSender = emailSender;
            this.configuration = configuration;
            this.gradeService = gradeService;
            this.contestService = contestService;
            this.essayService = essayService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();

            var lastcontestId = this.contestService.GetLastContestId();
            var essaysIdsOrderedByPoints = this.gradeService.GetEssaysIdsOrderedByPoints();
            viewModel.Essays = this.essayService.GetBestEssaysFromLastContest<EssayViewModel>(lastcontestId, essaysIdsOrderedByPoints);

            return this.View(viewModel);
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
            var viewModel = new EmailViewModel();

            return this.View(viewModel);
        }

        [ValidateRecaptcha]
        [HttpPost]
        public async Task<IActionResult> Contact(EmailViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            await this.emailSender.SendEmailAsync("essaycompetitionsender@abv.bg", viewModel.Email, "essaycompetitionsender@abv.bg", viewModel.Subject, viewModel.Message);
            this.TempData["FormResult"] = FormResult;
            return this.RedirectToAction("Contact");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
