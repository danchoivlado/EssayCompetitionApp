namespace EssayCompetition.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using EssayCompetition.Services.Data.ContestServices;
    using EssayCompetition.Services.Data.TeacherServices;
    using EssayCompetition.Web.ViewModels.Administration.Contest;
    using EssayCompetition.Web.ViewModels.Administration.Contest.Shared;
    using Microsoft.AspNetCore.Mvc;

    public class ContestController : AdministrationController
    {
        public const int PageSize = 5;
        private readonly IContestService contestService;
        private readonly ITeacherService teacherService;

        public ContestController(IContestService contestService, ITeacherService teacherService)
        {
            this.contestService = contestService;
            this.teacherService = teacherService;
        }

        public IActionResult Index(IndexViewModel viewModel)
        {
            viewModel ??= new IndexViewModel();
            viewModel.Pager = new PagerViewModel();
            viewModel.Pager.CurrentPage = viewModel.Pager.CurrentPage <= 0 ? 1 : viewModel.Pager.CurrentPage;

            viewModel.Contests = this.contestService.GetAllContestsRange<ContestViewModel>(viewModel.Pager.CurrentPage, PageSize);
            viewModel.Pager.PagesCount = (int)Math.Ceiling((double)this.contestService.GetContestsCount() / PageSize);

            return this.View(viewModel);
        }

        public IActionResult Details(int id)
        {
            var viewModel = this.contestService.GetContestDetails<DetailsViewModel>(id);
            return this.View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            if (!this.contestService.HasContestWithId(id))
            {
                return this.NotFound();
            }

            var viewModel = this.contestService.GetContestDetails<EditViewModel>(id);
            viewModel.AllAvilableCategory = this.teacherService.GetAllAvilableCategories<CategoryDropDownViewModel>();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel editViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                editViewModel.AllAvilableCategory = this.teacherService.GetAllAvilableCategories<CategoryDropDownViewModel>();
                return this.View(editViewModel);
            }

            if (!this.contestService.HasContestWithId(editViewModel.Id))
            {
                return this.NotFound();
            }

            await this.contestService.UpdateContestAsync(
                editViewModel.StartTime,
                editViewModel.EndTime,
                editViewModel.Name,
                editViewModel.Description,
                editViewModel.CategoryId,
                editViewModel.Id);

            return this.RedirectToAction("Index");
        }
    }
}
