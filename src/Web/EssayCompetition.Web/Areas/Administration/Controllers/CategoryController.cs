namespace EssayCompetition.Web.Areas.Administration.Controllers
{

    using Microsoft.AspNetCore.Mvc;

    public class CategoryController : AdministrationController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}