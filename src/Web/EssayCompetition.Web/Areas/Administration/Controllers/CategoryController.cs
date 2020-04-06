namespace EssayCompetition.Web.Areas.Administration.Controllers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using EssayCompetition.Services.Data.CategoryServices;
    using EssayCompetition.Web.ViewModels.Administration.Category;
    using EssayCompetition.Web.ViewModels.Administration.Category.Shared;
    using Microsoft.AspNetCore.Mvc;

    public class CategoryController : AdministrationController
    {
        private const int PageSize = 10;
        private readonly ICategoryService categoryService;
        private readonly Cloudinary cloudinary;

        public CategoryController(ICategoryService categoryService, Cloudinary cloudinary)
        {
            this.categoryService = categoryService;
            this.cloudinary = cloudinary;
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

        public IActionResult Create(string imageUrl)
        {
            var viewModel = new CreateViewModel();
            if (!string.IsNullOrEmpty(imageUrl))
            {
                viewModel.ImageUrl = imageUrl;
            }

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            if (viewModel.Content != null)
            {
                viewModel.ImageUrl = await this.categoryService.UploadImageToCloudinaryAsync(viewModel.Content);
            }

            await this.categoryService.CreateAsync(viewModel.Title, viewModel.Description, viewModel.ImageUrl);

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

        public async Task<IActionResult> Edit(int id)
        {
            if (!await this.categoryService.HasWithIdAsync(id))
            {
                return this.RedirectToAction("Index");
            }

            var viewModel = this.categoryService.GetWithId<EditViewModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            await this.categoryService.UpdateAsync(viewModel.Id, viewModel.Title, viewModel.Description, viewModel.ImageUrl);

            return this.RedirectToAction("Index");
        }
    }
}
