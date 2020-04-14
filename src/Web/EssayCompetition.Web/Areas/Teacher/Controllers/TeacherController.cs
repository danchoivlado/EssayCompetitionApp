namespace EssayCompetition.Web.Areas.Teacher.Controllers
{
    using EssayCompetition.Common;
    using EssayCompetition.Web.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.TeacherRoleName)]
    [Area("Teacher")]
    public class TeacherController : BaseController
    {
    }
}
