namespace EssayCompetition.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;
    using EssayCompetition.Services.Data.CategoryServices;
    using EssayCompetition.Web.ViewModels.Administration.Category;
    using EssayCompetition.Web.ViewModels.Administration.Category.Shared;
    using Microsoft.AspNetCore.Mvc;

    public class CategoryController : AdministrationController
    {
        private const int PageSize = 10;
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IActionResult Index(IndexViewModel model)
        {
            model ??= new IndexViewModel();
            model.Pager ??= new PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;

            model.AllCategories = this.categoryService.GetAll<CategoryViewModel>(model.Pager.CurrentPage, PageSize);

            model.Pager.PagesCount = (int)Math.Ceiling(this.categoryService.GetCount() / (double)PageSize);
            return this.View(model);
        }

        public IActionResult Create()
        {
            var model = new CreateViewModel();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.categoryService.CreateAsync(model.Title, model.Description, model.ImageUrl);

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await this.categoryService.HasWithIdAsync(id))
            {
                await this.categoryService.DeleteAsync(id);
            }

            return this.RedirectToAction("Index");
        }
    }
}
