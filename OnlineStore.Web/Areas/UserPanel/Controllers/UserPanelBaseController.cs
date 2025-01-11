using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class UserPanelBaseController : Controller
    {
        public string SuccessMessage = "SuccessMessage";
        public string WarningMessage = "WarningMessage";
        public string InfoMessage = "InfoMessage";
        public string ErrorMessage = "ErrorMessage";
    }
}
