using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Security;

namespace OnlineStore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [PermissionChecker("Admin")]
    public class AdminBaseController : Controller
    {
        public string SuccessMessage = "SuccessMessage";
        public string WarningMessage = "WarningMessage";
        public string InfoMessage = "InfoMessage";
        public string ErrorMessage = "ErrorMessage";
    }
}
