using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Extensions;
using OnlineStore.Application.Sender.Interface;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Application.Static;
using OnlineStore.Domain.Shared;
using OnlineStore.Domain.ViewModel.ContactUs;

namespace OnlineStore.Web.Areas.Admin.Controllers.ContactUs
{
    public class AdminContactUsController(
        IContactUsService contactUsService,
        IViewRenderService renderService) : AdminBaseController
    {
        #region ContactUs List
        public async Task<IActionResult> List(FilterContactUsViewModel filter)
        {
            FilterContactUsViewModel model = await contactUsService.FilterContactUsAsync(filter);
            return View(model);
        }
        #endregion

        #region Detail
        public async Task<IActionResult> Detail(int id)
        {
            DetailContactUsViewModel model = await contactUsService.GetContactUsByIdAsync(id);
            return View(model);
        }
        #endregion

        #region Answer to contactus
        [HttpPost]
        public async Task<IActionResult> Answer(AnswerContactUsViewModel model)
        {
            model.AnswerUserId = User.GetUserById();
            #region Validation
            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = WarningMessages.InserField;
                return View(model);
            }
            #endregion
            AnswerContactUsResult result = await contactUsService.AnswerContactUsAsync(model);
            switch (result)
            {
                case AnswerContactUsResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.ContactEmail;
                    return RedirectToAction("List","AdminContactUs");
                default:
                case AnswerContactUsResult.NotFoundContact:
                    TempData[WarningMessage] = WarningMessages.NotFoundData;
                    return RedirectToAction("List", "AdminContactUs");
            }


        }
        #endregion
    }
}
