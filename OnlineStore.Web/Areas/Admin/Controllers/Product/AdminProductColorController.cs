using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Security;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.Shared;
using OnlineStore.Domain.ViewModel.Admin.Color;

namespace OnlineStore.Web.Areas.Admin.Controllers.Product
{
    
    public class AdminProductColorController(IProductColorService colorService) : AdminBaseController
    {
        #region List
        [PermissionChecker("AdminProduct")]
        public async Task<IActionResult> List(int productId)
        {
            ViewData["ProductId"]= productId;
            List<ProductColorViewModel>model=await colorService.GetProductColorByProductIdAsync(productId);
            return View(model);
        }
        #endregion

        #region Create
        [HttpGet("/Admin/Create-ProductColor/{productId}"),PermissionChecker("AddProduct")]
        public async Task<IActionResult> Create(int productId)
        {
            return PartialView(new CreateProductColorViewModel()
            {
                ProductId = productId
            });
        }

        [HttpPost("/Admin/Create-ProductColor/{productId}"), PermissionChecker("AddProduct")]
        public async Task<IActionResult> Create(CreateProductColorViewModel model)
        {
            #region Validation
            if(!ModelState.IsValid)
            {
                TempData[WarningMessage]=WarningMessages.InserField;
                return RedirectToAction("List", "AdminProductColor", new { productId = model.ProductId });
            }
            #endregion
            CreateProductColorResult result = await colorService.CreateProductColorAsync(model);
            switch (result)
            {
                case CreateProductColorResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.CreateData;
                    return RedirectToAction("List", "AdminProductColor", new { productId = model.ProductId });
            }
            return RedirectToAction("List", "AdminProductColor", new {productId = model.ProductId});
        }
        #endregion

        #region Delete
        [PermissionChecker("DeleteProduct")]
        public async Task Delete(int id)
        {
            await colorService.DeleteProductColorAsync(id);
        }
        #endregion
    }
}
