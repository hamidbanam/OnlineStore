using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.Web.Areas.Admin.Components
{
    public class LeftSideBarViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("LeftSideBar");
        }
    }
}
