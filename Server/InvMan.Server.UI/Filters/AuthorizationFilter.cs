using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using InvMan.Server.Application;
using InvMan.Server.Domain.Models;

namespace InvMan.Server.UI.Filters
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        private readonly ClientUsersManager _usersManager;

        public AuthorizationFilter(ClientUsersManager usersManager)
        {
            _usersManager = usersManager;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var query = context.HttpContext.Request.Query;
            var request = context.HttpContext.Request;
            var unauthorizedResult = new UnauthorizedObjectResult(
                new BadRequestErrorMessage {
                    Error = "Wrong API key",
                    Description = "Provide an API key as an API header or via api query"
                }
            );

            string api = query["api"].Count == 0 ? request.Headers["API"] : query["api"];

            var callingUser = _usersManager.FindByApi(api);

            if (callingUser == null)
                context.Result = unauthorizedResult;
        }
    }
}
