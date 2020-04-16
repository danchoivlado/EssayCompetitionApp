namespace EssayCompetition.Web.Areas.Contest.Controllers
{
    using EssayCompetition.Common;
    using EssayCompetition.Web.Controllers;
    using EssayCompetition.Web.ValidationAttributes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [HasContest]
    [Authorize(Roles = GlobalConstants.ContestRoleName)]
    [Area("Contest")]
    public class ContestController : BaseController
    {
    }
}
