using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Extensions;
using OnlineStore.Application.Services.Implentation;
using OnlineStore.Application.Services.Interface;

namespace OnlineStore.Web.Components
{
    public class ProfileDetailViewComponent(IUserService userService):ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string userName = HttpContext.User.GetUserByUserName();
            ViewData["User"] = await userService.GetUserByUserNameOrMobile(userName);
            return View("ProfileDetail");
        }
    }
}
