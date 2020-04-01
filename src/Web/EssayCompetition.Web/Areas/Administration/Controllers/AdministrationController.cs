namespace EssayCompetition.Web.Areas.Administration.Controllers
{
    using EssayCompetition.Common;
    using EssayCompetition.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
