﻿namespace EssayCompetition.Web.Controllers
{
    using EssayCompetition.Services.Data.ContestServices;
    using EssayCompetition.Services.Data.EssayServices;
    using EssayCompetition.Services.Data.GradeServices;
    using EssayCompetition.Services.Data.UsersServices;
    using EssayCompetition.Web.ViewModels.ContestHome;
    using Microsoft.AspNetCore.Mvc;
    using EssayCompetition.Web.ViewModels.ContestHome.Shared;
    using System;
    using System.Collections.Generic;

    public class ContestController : BaseController
    {
        private const int PageSize = 5;
        private readonly IContestService contestService;
        private readonly IUsersService usersService;
        private readonly IEssayService essayService;
        private readonly IGradeService gradeService;

        public ContestController(
            IContestService contestService,
            IUsersService usersService,
            IEssayService essayService,
            IGradeService gradeService)
        {
            this.contestService = contestService;
            this.usersService = usersService;
            this.essayService = essayService;
            this.gradeService = gradeService;
        }

        public IActionResult ById(int id, ByIdViewModel viewModel)
        {
            viewModel ??= new ByIdViewModel();
            viewModel.Pager ??= new PagerViewModel();
            viewModel.Pager.CurrentPage = viewModel.Pager.CurrentPage <= 0 ? 1 : viewModel.Pager.CurrentPage;

            var contestantIds = this.contestService.GetContestParticipantsIdsInRange(id, viewModel.Pager.CurrentPage, PageSize);
            viewModel.Pager.PagesCount = (int)Math.Ceiling((double)this.contestService.GetContestParticipantsCount(id) / PageSize);
            var allContestants = new List<ContestantViewModel>();

            foreach (var contestant in this.usersService.GetUsersWithIds<ContestantViewModel>(contestantIds))
            {
                contestant.EssayName = this.essayService.GetEssayName(contestant.Id, id);
                contestant.EssayPoints = this.gradeService.GetEssayPoints(this.essayService.GetEssaysId(contestant.Id, id));
                allContestants.Add(contestant);
            }

            viewModel.Contestants = allContestants;

            return this.View(viewModel);
        }
    }
}
