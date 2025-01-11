using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.Web.Areas.UserPanel.Components
{
    public class SideBarUserPanelViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("SideBarUserPanel");
        }
    }
}
