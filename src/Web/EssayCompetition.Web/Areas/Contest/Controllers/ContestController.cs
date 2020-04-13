namespace EssayCompetition.Web.Areas.Contest.Controllers
{
    using EssayCompetition.Common;
    using EssayCompetition.Web.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.ContestRoleName)]
    [Area("Contest")]
    public class ContestController : BaseController
    {
    }
}