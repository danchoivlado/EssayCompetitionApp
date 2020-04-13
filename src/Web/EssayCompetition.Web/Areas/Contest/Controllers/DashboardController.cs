namespace EssayCompetition.Web.Areas.Contest.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : ContestController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
