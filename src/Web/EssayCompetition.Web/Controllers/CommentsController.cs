// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EssayCompetition.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EssayCompetition.Data.Models;
    using EssayCompetition.Services.Data.CommentServices;
    using EssayCompetition.Services.Data.EssayServices;
    using EssayCompetition.Web.ViewModels.Essays;
    using EssayCompetition.Web.ViewModels.Essays.Shared;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class CommentsController : Controller
    {
        private readonly ICommentService commentService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEssayService essayService;

        public CommentsController(ICommentService commentService, UserManager<ApplicationUser> userManager,IEssayService essayService)
        {
            this.commentService = commentService;
            this.userManager = userManager;
            this.essayService = essayService;
        }

        // POST api/<controller>
        [Authorize]
        [HttpPost]
        public async Task Post([FromBody] CommentRequestViewModel input)
        {
            if (this.essayService.HasEssayWithId(input.EssayId) && string.IsNullOrEmpty(input.CommentContent) != true)
            {
                var userId = this.userManager.GetUserId(this.User);
                await this.commentService.AddCommentAsync(userId, input.EssayId, input.CommentContent);
            }
        }
    }
}
