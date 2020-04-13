namespace EssayCompetition.Web.Areas.Contest.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class CreateController : ContestController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
