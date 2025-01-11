using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Extensions;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.Shared;
using OnlineStore.Domain.ViewModel.ContactUs;

namespace OnlineStore.Web.Controllers
{
    public class ContactUsController(IContactUsService contactUsService) : SiteBaseController
    {
        [HttpGet("/contact-us")]
        public IActionResult CreateContactUs()
        {
            return View();
        }

        [HttpPost("/contact-us"),ValidateAntiForgeryToken]
          public async  Task<IActionResult> CreateContactUs(CreateContactUsViewModel model)
        {
            model.Ip=HttpContext.GetUserIp();
            #region Validation
            if(!ModelState.IsValid)
            {
                TempData[WarningMessage] = WarningMessages.InserField;
                return View(model);
            }
            #endregion
            ContactUsResult result=await contactUsService.CreateContactUsAsync(model);
            switch (result)
            {
                case ContactUsResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.CreateData;
                    return RedirectToAction("Index", "Home");
            
            }
            TempData[ErrorMessage] = ErrorMessages.OprationFaild;
            return View(model);
        }
    }
}
