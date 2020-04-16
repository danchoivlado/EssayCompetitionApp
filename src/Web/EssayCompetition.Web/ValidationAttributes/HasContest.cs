using EssayCompetition.Services.Data.ContestServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EssayCompetition.Web.ValidationAttributes
{
    public class HasContest : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var date = DateTime.Now;
            var userService = context.HttpContext.RequestServices.GetService(typeof(IContestService)) as IContestService;

            if (!userService.HasContextNow(date))
            {
                context.Result = new ForbidResult();
                return;
            }

            return;
        }
    }
}
