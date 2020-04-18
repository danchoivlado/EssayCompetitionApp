using Microsoft.AspNetCore.Mvc;

namespace EssayCompetition.Web.Areas.Contest.Controllers
{
    public class SignController : ContestController
    {


        public IActionResult Index()
        {
            return this.View();
        }
    }
}