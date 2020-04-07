namespace EssayCompetition.Web.Areas.Teacher.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : TeacherController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}