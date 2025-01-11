using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineStore.Application.Extensions;
using OnlineStore.Application.Services.Interface;

namespace OnlineStore.Application.Security
{
    public class PermissionChecker : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private IUserService _userService;
        private string _permissionTitle;
        public PermissionChecker(string permissionTitle)
        {
            _permissionTitle = permissionTitle;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            _userService = (IUserService)context.HttpContext.RequestServices.GetService(typeof(IUserService));
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                int userId = context.HttpContext.User.GetUserById();
                if (!await _userService.CheckPermissionAsync(_permissionTitle,userId))
                {
                    context.Result = new RedirectResult("/Home/AccessDenied");
                }
            }
            else
            {
                context.Result = new RedirectResult("/Login");
            }
        }
    }

    

}


