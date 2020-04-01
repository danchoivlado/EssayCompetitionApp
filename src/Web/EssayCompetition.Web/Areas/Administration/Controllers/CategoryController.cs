namespace EssayCompetition.Web.Areas.Administration.Controllers
{
    using EssayCompetition.Services.Data.CategoryServices;
    using EssayCompetition.Web.ViewModels.Administration.Category;
    using Microsoft.AspNetCore.Mvc;

    public class CategoryController : AdministrationController
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var model = new IndexViewModel();
            model.AllCategories = this.categoryService.GetAll<CategoryViewModel>();
            return this.View(model);
        }
    }
}