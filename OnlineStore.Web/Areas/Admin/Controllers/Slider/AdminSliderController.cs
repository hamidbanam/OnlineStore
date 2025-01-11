using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Security;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.Shared;
using OnlineStore.Domain.ViewModel.Slider.HomePageSlider;
using System.Security.Permissions;

namespace OnlineStore.Web.Areas.Admin.Controllers.Slider
{
    public class AdminSliderController(IClientService clientService) : AdminBaseController
    {
        #region List
        [Route("/admin-slider"), PermissionChecker("AdminClient")]
        public async Task<IActionResult> List()
        {
            List<HomePageSliderViewModel> model = await clientService.GetHomePageSliderAsync();
            return View(model);
        }
        #endregion

        #region Create
        [HttpGet("/admin-slider/create"), PermissionChecker("AddClient")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost("/admin-slider/create"), ValidateAntiForgeryToken, PermissionChecker("AddClient")]
        public async Task<IActionResult> Create(CreateHomePageSliderViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = WarningMessages.InserField;
                return View(model);
            }
            #endregion
            CreateHomePageSliderResult result = await clientService.CreateHomePageSliderAsync(model);
            switch (result)
            {
                case CreateHomePageSliderResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.CreateData;
                    return RedirectToAction("List", "AdminSlider");
                case CreateHomePageSliderResult.ImageInvalid:
                    TempData[WarningMessage] = WarningMessages.ImageInvalid;
                    return View(model);
                case CreateHomePageSliderResult.MoreThanAllowed:
                    TempData[WarningMessage] = WarningMessages.MoreThanAllowed;
                    return View(model);
            }
            TempData[WarningMessage] = WarningMessages.InserField;
            return View(model);
        }
        #endregion

        #region Edit
        [HttpGet("/admin-slider/Edit"), PermissionChecker("EditClient")]
        public async Task<IActionResult> Edit(int sliderId)
        {
            EditHomePageSliderViewModel model = await clientService.EditSliderHomePageAsync(sliderId);
            return View(model);
        }

        [HttpPost("/admin-slider/Edit"), ValidateAntiForgeryToken, PermissionChecker("EditClient")]
        public async Task<IActionResult> Edit(EditHomePageSliderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = WarningMessages.InserField;
                return View(model);
            }
            EditHomePageSliderResult result = await clientService.UpdateHomePageSliderAsync(model);
            switch (result)
            {
                case EditHomePageSliderResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.CreateData;
                    return RedirectToAction("List", "AdminSlider");
                case EditHomePageSliderResult.NotFound:
                    TempData[WarningMessage] = WarningMessages.NotFoundData;
                    return View(model);
            }
            TempData[WarningMessage] = WarningMessages.InserField;
            return View(model);
        }
        #endregion

        #region Delete
        [PermissionChecker("DeleteClient")]
        public async Task Delete(int id)
        {
            if (id != null)
                await clientService.DeleteSliderHomePageAsync(id);
        }
        #endregion

    }
}
