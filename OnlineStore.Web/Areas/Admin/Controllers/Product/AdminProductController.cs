using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Security;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.Shared;
using OnlineStore.Domain.ViewModel.Admin.Product;

namespace OnlineStore.Web.Areas.Admin.Controllers.Product
{
    public class AdminProductController(IProductService productService, ICategoryService categoryService) : AdminBaseController
    {
        #region List
        [PermissionChecker("AdminProduct")]
        public async Task<IActionResult> List(FilterProductViewModel filter)
        {
            var model = await productService.GetFilterProductsAsync(filter);
            return View(model);
        }
        #endregion

        #region Create
        [HttpGet("/Admin/Create-Product"),PermissionChecker("AddProduct")]
        public async Task<IActionResult> Create()
        {
            return View(new CreateProductViewModel()
            {
                Categories = await categoryService.GetAllCategoryAsync()
            });
        }

        [HttpPost("/Admin/Create-Product"), ValidateAntiForgeryToken, PermissionChecker("AddProduct")]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
            {
                model.Categories = await categoryService.GetAllCategoryAsync();
                return View(model);
            }
            #endregion
            CreateProductResult result = await productService.CreateProductAsync(model);
            switch (result)
            {
                case CreateProductResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.CreateData;
                    return RedirectToAction("List", "AdminProduct");
                case CreateProductResult.SlugExits:
                    TempData[WarningMessage] = WarningMessages.DublicatedSlug;
                    model.Categories = await categoryService.GetAllCategoryAsync();
                    return View(model);
            }
            model.Categories = await categoryService.GetAllCategoryAsync();
            return View(model);
        }
        #endregion

        #region Edit
        [HttpGet("/Admin/Edit-Product"),PermissionChecker("EditProduct")]
        public async Task<IActionResult> Edit(int productId)
        {
            var model = await productService.GetEditProductAsync(productId);
            return View(model);
        }

        [HttpPost("/Admin/Edit-Product"), ValidateAntiForgeryToken, PermissionChecker("EditProduct")]
        public async Task<IActionResult> Edit(EditProductViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
            {
                model.Categories = await categoryService.GetAllCategoryAsync();
                return View(model);
            }
            #endregion
            EditProductResult result = await productService.UpdateProductAsync(model);
            switch (result)
            {
                case EditProductResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.UpdateData;
                    return RedirectToAction("List", "AdminProduct");
                case EditProductResult.SlugExits:
                    TempData[WarningMessage] = WarningMessages.DublicatedSlug;
                    model.Categories = await categoryService.GetAllCategoryAsync();
                    return View(model);
                case EditProductResult.NotFound:
                    TempData[WarningMessage] = WarningMessages.NotFoundData;
                    model.Categories = await categoryService.GetAllCategoryAsync();
                    return View(model);
            }
            model.Categories = await categoryService.GetAllCategoryAsync();
            return View(model);
        }
        #endregion

        #region Delete
        [PermissionChecker("DeleteProduct")]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteProductResult result=await productService.DeleteProductAsync(id);
            switch (result)
            {
                case DeleteProductResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.DeleteData;
                    return RedirectToAction("List", "AdminProduct");
                case DeleteProductResult.NotFound:
                    TempData[WarningMessage] = WarningMessages.NotFoundData;
                    return RedirectToAction("List", "AdminProduct");
            }
            TempData[WarningMessage] = WarningMessages.InserField;
            return RedirectToAction("List","AdminProduct");
        }
        #endregion
    }
}
