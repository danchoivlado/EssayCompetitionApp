namespace EssayCompetition.Web.Areas.Contest.Controllers
{
    using System;

    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Data.EssayServices;
    using EssayCompetition.Services.Data.GradeServices;
    using EssayCompetition.Web.ViewModels.Contest.MyEssay;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class MyEssayController : ContestController
    {
        private const int PageSize = 5;
        private readonly IEssayService essayService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IGradeService gradeService;

        public MyEssayController(IEssayService essayService, UserManager<ApplicationUser> userManager, IGradeService gradeService)
        {
            this.essayService = essayService;
            this.userManager = userManager;
            this.gradeService = gradeService;
        }

        public IActionResult Index(IndexViewModel viewModel)
        {
            var userId = this.userManager.GetUserId(this.User);
            viewModel ??= new IndexViewModel();
            viewModel.Pager ??= new PagerViewModel();
            viewModel.Pager.CurrentPage = viewModel.Pager.CurrentPage <= 0 ? 1 : viewModel.Pager.CurrentPage;

            viewModel.Essays = this.essayService.GetEssaysFromUserWithIdInRange<EssayViewModel>(userId, viewModel.Pager.CurrentPage, PageSize);
            viewModel.Pager.PagesCount = (int)Math.Ceiling((double)this.essayService.GetUserEssaysCount(userId) / PageSize);

            return this.View(viewModel);
        }

        public IActionResult Details(int id)
        {
            var viewModel = this.essayService.GetEssayDetails<DetailsViewModel>(id);
            viewModel.Grade = this.gradeService.GetGradeDetails<GradeViewModel>(id);
            return this.View(viewModel);
        }
    }
}
