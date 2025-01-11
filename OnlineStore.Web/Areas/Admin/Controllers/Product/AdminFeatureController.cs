using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Security;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.Shared;
using OnlineStore.Domain.ViewModel.Admin.Feature;
using OnlineStore.Web.Controllers;

namespace OnlineStore.Web.Areas.Admin.Controllers.Product
{
    public class AdminFeatureController(IFeatureService featureService) : AdminBaseController
    {
        #region List
        [PermissionChecker("AdminProduct")]
        public async Task<IActionResult> List(FilterFeatureViewModel filter)
        {

            var model = await featureService.GetFilterFeatureAsync(filter);
            return View(model);
        }
        #endregion

        #region Create
        [HttpGet("/Admin/Create-Feature"), PermissionChecker("AddProduct")]
        public async Task<IActionResult> Create()
        {
            return PartialView();
        }

        [HttpPost("/Admin/Create-Feature"), ValidateAntiForgeryToken, PermissionChecker("AddProduct")]
        public async Task<IActionResult> Create(CreateFeatureViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = WarningMessages.InserField;
                return RedirectToAction("List", "AdminFeature");
            }
            #endregion

            CreateFeatureResult result = await featureService.CreateFeatureAsync(model);
            switch (result)
            {
                case CreateFeatureResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.CreateData;
                    return RedirectToAction("List", "AdminFeature");
            }
            TempData[WarningMessage] = WarningMessages.InserField;
            return RedirectToAction("List", "AdminFeature");
        }
        #endregion

        #region Edit
        [HttpGet("/Admin/Edit-Feature/{featureId}"),PermissionChecker("EditProduct")]
        public async Task<IActionResult> Edit(int featureId)
        {
            if (featureId == null || featureId <= 0)
            {
                return NotFound();
            }
            EditFeatureViewModel model = await featureService.GetEditFeatureAsync(featureId);
            return PartialView(model);
        }

        [HttpPost("/Admin/Edit-Feature/{featureId}"), ValidateAntiForgeryToken, PermissionChecker("EditProduct")]
        public async Task<IActionResult> Edit(EditFeatureViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = WarningMessages.InserField;
                return RedirectToAction("List", "AdminFeature");
            }
            #endregion
            EditFeatureResult result = await featureService.EditFeatureAsync(model);
            switch (result)
            {
                case EditFeatureResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.CreateData;
                    return RedirectToAction("List", "AdminFeature");
                case EditFeatureResult.NotFound:
                    TempData[WarningMessage] = WarningMessages.NotFoundData;
                    return RedirectToAction("List", "AdminFeature");
            }
            TempData[WarningMessage] = WarningMessages.InserField;
            return RedirectToAction("List", "AdminFeature");
        }
        #endregion  

        #region Delete
        [PermissionChecker("DeleteProduct")]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteFeatureResult result = await featureService.DeleteFeatureAsync(id);
            switch (result)
            {
                case DeleteFeatureResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.DeleteData;
                    return RedirectToAction("List", "AdminFeature");
                case DeleteFeatureResult.NotFound:
                    TempData[WarningMessage] = WarningMessages.NotFoundData;
                    return RedirectToAction("List", "AdminFeature");
            }
            TempData[WarningMessage] = WarningMessages.InserField;
            return RedirectToAction("List", "AdminFeature");
        }
        #endregion

    }
}
