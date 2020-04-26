namespace EssayCompetition.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EssayCompetition.Services.Data.CommentServices;
    using EssayCompetition.Services.Data.ContestServices;
    using EssayCompetition.Services.Data.EssayServices;
    using EssayCompetition.Services.Data.GradeServices;
    using EssayCompetition.Services.Data.UserAdditionalInfoServices;
    using EssayCompetition.Web.ViewModels.Essays;
    using EssayCompetition.Web.ViewModels.Essays.Shared;
    using Microsoft.AspNetCore.Mvc;

    public class EssayController : BaseController
    {
        private const int PageSize = 5;
        private readonly IGradeService gradeService;
        private readonly IContestService contestService;
        private readonly IEssayService essayService;
        private readonly ICommentService commentService;
        private readonly IUserAdditionalInfoService userAdditionalInfoService;

        public EssayController(
            IGradeService gradeService,
            IContestService contestService,
            IEssayService essayService,
            ICommentService commentService,
            IUserAdditionalInfoService userAdditionalInfoService)
        {
            this.gradeService = gradeService;
            this.contestService = contestService;
            this.essayService = essayService;
            this.commentService = commentService;
            this.userAdditionalInfoService = userAdditionalInfoService;
        }

        public IActionResult Index(IndexViewModel viewModel)
        {
            viewModel ??= new IndexViewModel();
            viewModel.Pager ??= new PagerViewModel();
            viewModel.Pager.CurrentPage = viewModel.Pager.CurrentPage <= 0 ? 1 : viewModel.Pager.CurrentPage;
            if (this.essayService.HasAnyGradedEssay())
            {
                viewModel.Essays = this.essayService.GetEssaysInRange<EssayViewModel>(viewModel.Pager.CurrentPage, PageSize);
                viewModel.GroupedEssays = viewModel.Essays.GroupBy(x => x.ContestId);
            }
            else
            {
                viewModel.Essays = new List<EssayViewModel>();
                viewModel.GroupedEssays = new List<List<EssayViewModel>>();
            }

            viewModel.Pager.PagesCount = (int)Math.Ceiling((double)this.essayService.GetEssaysCount() / PageSize);

            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var viewModel = this.essayService.GetEssayDetails<EssayViewModel>(id);
            var comments = new List<CommentViewModel>();
            foreach (var comment in this.commentService.GetCommentsFromEssay<CommentViewModel>(id))
            {
                if (this.userAdditionalInfoService.HasUserAdditionalInfoWithId(comment.UserId))
                {
                    comment.UserImage = this.userAdditionalInfoService.GetUserProfilePicture(comment.UserId);
                }

                comments.Add(comment);
            }

            viewModel.Comments = comments.OrderByDescending(x => x.CreatedOn);

            return this.View(viewModel);
        }
    }
}
