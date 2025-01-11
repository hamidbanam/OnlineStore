using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Security;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.Shared;
using OnlineStore.Domain.ViewModel.Admin.ProductFeature;

namespace OnlineStore.Web.Areas.Admin.Controllers.Product
{
    public class AdminProductFeatureController(IProductFeatureService productFeature,IFeatureService featureService) : AdminBaseController
    {
        #region List
        [PermissionChecker("AdminProduct")]
        public async Task<IActionResult> List(FilterProductFeatureViewModel filter)
        {
            var model = await productFeature.FilterProductFeatureAsync(filter);
            return View(model);
        }
        #endregion

        #region Create
        [HttpGet("/Admin/Create-Feature/{productId}"),PermissionChecker("AddProduct")]
        public async Task<IActionResult> Create(int productId)
        {
            if (!await productFeature.HasProductAsync(productId))
            {
                return NotFound();
            }
            return PartialView(new CreateProductFeatureViewModel()
            {
                Feature=await featureService.GetAllFeature(),
            });
        } 
        [HttpPost("/Admin/Create-Feature/{productId}"), PermissionChecker("AddProduct"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductFeatureViewModel model)
        {
            #region Validation
            if(!ModelState.IsValid)
            {
                TempData[WarningMessage] = WarningMessages.InserField;
                return RedirectToAction("List","AdminProductFeature",new {productId = model.ProductId});
            }
            #endregion
            CreateProductFeatureResult result=await productFeature.CreateProductFeatureAsync(model);
            switch (result)
            {
                case CreateProductFeatureResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.CreateData;
                    return RedirectToAction("List", "AdminProductFeature", new { productId = model.ProductId });
                case CreateProductFeatureResult.ProductNotFound:
                    TempData[WarningMessage] = WarningMessages.NotFoundData;
                    return RedirectToAction("List", "AdminProductFeature", new { productId = model.ProductId });
                case CreateProductFeatureResult.FeatureNotFound:
                    TempData[WarningMessage] = WarningMessages.NotFoundData;
                    return RedirectToAction("List", "AdminProductFeature", new { productId = model.ProductId });
            }
            return RedirectToAction("List", "AdminProductFeature", new { productId = model.ProductId });
        }
        #endregion

        #region Edit
        [HttpGet("/Admin/Edit-ProductFeature/{featureId}"),PermissionChecker("EditProduct")]
        public async Task<IActionResult> Edit(int featureId)
        {
            EditProductFeatureViewModel model =await productFeature.EditProductFeatureAsync(featureId);
            return PartialView(model);
        }  

        [HttpPost("/Admin/Edit-ProductFeature/{featureId}"),PermissionChecker("EditProduct"),ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProductFeatureViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = WarningMessages.InserField;
                return RedirectToAction("List", "AdminProductFeature", new { productId = model.ProductId });
            }
            #endregion
            EditProductFeatureResult result=await productFeature.UpdateProductFeatureAsync(model);
            switch (result)
            {
                case EditProductFeatureResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.UpdateData;
                    return RedirectToAction("List", "AdminProductFeature", new { productId = model.ProductId });
                case EditProductFeatureResult.NotFound:
                    TempData[WarningMessage] = WarningMessages.NotFoundData;
                    return RedirectToAction("List", "AdminProductFeature", new { productId = model.ProductId });
            }
            return RedirectToAction("List", "AdminProductFeature", new { productId = model.ProductId });
        }
        #endregion

        #region Delete
        [PermissionChecker("DeleteProduct")]
        public async Task Delete(int id)
        {
            await productFeature.DeleteProductFeatureAsync(id);
        }
        #endregion
    }
}
