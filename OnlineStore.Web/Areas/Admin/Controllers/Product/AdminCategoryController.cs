using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Security;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.Shared;
using OnlineStore.Domain.ViewModel.Admin.Category;

namespace OnlineStore.Web.Areas.Admin.Controllers.Product
{
    public class AdminCategoryController(ICategoryService categoryService) : AdminBaseController
    {
        #region List
        [PermissionChecker("AdminProduct")]
        public async Task<IActionResult> List(FilterCategoryViewModel filter)
        {
            var model = await categoryService.GetFilterCategoryAsync(filter);
            return View(model);
        }
        #endregion

        #region Create
        [HttpGet("/Admin/Create-Category"),PermissionChecker("AddProduct")]
        public async Task<IActionResult> Create()
        {
            return PartialView(new CreateCategoryViewModel()
            {
                Categories = await categoryService.GetAllCategoryAsync()
            });
        }

        [HttpPost("/Admin/Create-Category"), ValidateAntiForgeryToken, PermissionChecker("AddProduct")]
        public async Task<IActionResult> Create(CreateCategoryViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = WarningMessages.InserField;
                return RedirectToAction("List", "AdminCategory");
            }
            #endregion
            CreateCategoryResult result = await categoryService.CreateCategoryAsync(model);
            switch (result)
            {
                case CreateCategoryResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.CreateData;
                    return RedirectToAction("List", "AdminCategory");
            }
            TempData[WarningMessage] = WarningMessages.InserField;
            return RedirectToAction("List", "AdminCategory");
        }
        #endregion

        #region Edit
        [HttpGet("/Admin/Edit-Category/{categoryId}"), PermissionChecker("EditProduct")]
        public async Task<IActionResult> Edit(int categoryId)
        {
            EditCategoryViewModel model = await categoryService.GetEditCategoryAsync(categoryId);
            return PartialView(model);
        }

        [HttpPost("/Admin/Edit-Category/{categoryId}"), ValidateAntiForgeryToken, PermissionChecker("EditProduct")]
        public async Task<IActionResult> Edit(EditCategoryViewModel model)
        {
            model.Categories = await categoryService.GetAllCategoryAsync();
            #region Validation
            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = WarningMessages.InserField;
                return RedirectToAction("List", "AdminCategory");
            }
            #endregion
            EditCategoryResult result = await categoryService.EditCategoryAsync(model);
            switch (result)
            {
                case EditCategoryResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.UpdateData;
                    return RedirectToAction("List", "AdminCategory");
                case EditCategoryResult.NotFound:
                    TempData[WarningMessage] = WarningMessages.NotFoundData;
                    return RedirectToAction("List", "AdminCategory");
            }
            TempData[WarningMessage] = WarningMessages.InserField;
            return RedirectToAction("List", "AdminCategory");
        }
        #endregion

        #region Delete
        [PermissionChecker("DeleteProduct")]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteCategoryResult result = await categoryService.DeleteCategoryAsync(id);
            switch (result)
            {
                case DeleteCategoryResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.DeleteData;
                    return RedirectToAction("List", "AdminCategory");
                case DeleteCategoryResult.NotFound:
                    TempData[WarningMessage] = WarningMessages.NotFoundData;
                    return RedirectToAction("List", "AdminCategory");
            }
            TempData[WarningMessage] = WarningMessages.InserField;
            return RedirectToAction("List", "AdminCategory");
        }
        #endregion
    }
}
