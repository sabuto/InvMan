using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using InvMan.Server.Domain.Models;

namespace InvMan.Server.UI.Filters
{
    public class AuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly UserManager<DesktopUser> _usersManager;

        public AuthorizationFilter(UserManager<DesktopUser> usersManager)
        {
            _usersManager = usersManager;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var query = context.HttpContext.Request.Query;
            var request = context.HttpContext.Request;
            var unauthorizedResult = new JsonResult(new { Error = "Invalid API key", StatusCode = 401 }) {
                StatusCode = 401
            };

            string api = query["api"].Count == 0 ? request.Headers["API"] : query["api"];

            var callingUser = await _usersManager.FindByIdAsync(api);

            if (callingUser == null)
                context.Result = unauthorizedResult;

            return;
        }
    }
}