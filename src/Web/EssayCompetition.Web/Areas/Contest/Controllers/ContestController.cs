namespace EssayCompetition.Web.Areas.Contest.Controllers
{
    using EssayCompetition.Common;
    using EssayCompetition.Web.Controllers;
    using EssayCompetition.Web.ValidationAttributes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [Area("Contest")]
    public class ContestController : BaseController
    {
    }
}
