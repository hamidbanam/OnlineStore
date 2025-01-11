using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Security;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.Shared;
using OnlineStore.Domain.ViewModel.Admin.Gallary;

namespace OnlineStore.Web.Areas.Admin.Controllers.Product
{
    public class AdminProductGallaryController(IProductGallaryService gallaryService) : AdminBaseController
    {
        #region List
        [PermissionChecker("AdminProduct")]
        public async Task<IActionResult> List(int productId)
        {
            ViewData["ProductId"] = productId;
            List<ProductGallaryViewModel> model = await gallaryService.GetGallaryByProductIdAsync(productId);
            return View(model);
        }
        #endregion

        #region Create
        [HttpGet("/Admin/Create-Gallary/{productId}"), PermissionChecker("AddProduct")]
        public async Task<IActionResult> Create(int productId)
        {
            return PartialView(new CreateProductGallaryViewModel()
            {
                ProductId = productId
            });
        }

        [HttpPost("/Admin/Create-Gallary/{productId}"), PermissionChecker("AddProduct"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductGallaryViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = WarningMessages.InserField;
                return RedirectToAction("List", "AdminProductGallary", new { productId = model.ProductId });
            }
            #endregion
            CreateProductGallaryResult result = await gallaryService.CreateProductGallaryAsync(model);
            switch (result)
            {
                case CreateProductGallaryResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.CreateData;
                    return RedirectToAction("List", "AdminProductGallary", new { productId = model.ProductId });
            }
            return RedirectToAction("List", "AdminProductGallary", new { productId = model.ProductId });
        }
        #endregion

        #region Edit
        [HttpGet("/Admin/Edit-Gallary/{gallaryId}"), PermissionChecker("EditProduct")]
        public async Task<IActionResult> Edit(int gallaryId)
        {
            EditProductGallartViewModel model = await gallaryService.EditProductGallaryAsync(gallaryId);
            return PartialView(model);
        }
        [HttpPost("/Admin/Edit-Gallary/{gallaryId}"), PermissionChecker("EditProduct"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProductGallartViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = WarningMessages.InserField;
                return RedirectToAction("List", "AdminProductGallary", new { productId = model.ProductId });
            }
            #endregion
            EditProductGallaryResult result = await gallaryService.UpdateProductGallary(model);
            switch (result)
            {
                case EditProductGallaryResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.UpdateData;
                    return RedirectToAction("List", "AdminProductGallary", new { productId = model.ProductId });
                case EditProductGallaryResult.NotFound:
                    TempData[WarningMessage] = WarningMessages.NotFoundData;
                    return RedirectToAction("List", "AdminProductGallary", new { productId = model.ProductId });
            }
            return RedirectToAction("List", "AdminProductGallary", new { productId = model.ProductId });
        }
        #endregion   

        #region Delete
        [PermissionChecker("DeleteProduct")]
        public async Task Delete(int id)
        {
            await gallaryService.DeleteProductGallaryAsync(id);
        }
        #endregion
    }
}
