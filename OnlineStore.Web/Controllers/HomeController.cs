using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application
    .Extensions;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Data.Context;
using OnlineStore.Domain.Shared;
using OnlineStore.Domain.ViewModel.Client.Product;
using OnlineStore.Domain.ViewModel.Client.ProductComment;

namespace OnlineStore.Web.Controllers
{
    public class HomeController(IUserService userService,
        IProductService productService,
        IProductCommentService commentService,
        IProductColorService colorService,
        OnlineStoreContext context
        ) : SiteBaseController
    {
        #region Home
        public async Task<IActionResult> Index()
        {
            return View();
        }
        #endregion

        #region AccessDenied
        public IActionResult AccessDenied()
        {
            return View();
        }
        #endregion

        #region Product
        #region ProductList
        public async Task<IActionResult> ProductList(FilterClientProductViewModel filter, string? title)
        {
            if (title != null)
            {
                filter.Tilte = title;
            }
            var model = await productService.GetFilterProductsAsync(filter);

            return View(model);
        }
        #endregion

        #region ProductDetail
        [HttpGet("/Product-Detail/{slug}")]
        public async Task<IActionResult> ProductDetail(string slug)
        {
            ProductDetailViewModel product = await productService.GetProductDetailAsync(slug);
            if (product == null)
            {
                return NotFound();
            }
            TempData["Slug"] = slug;
            return View(product);
        }
        #endregion

        #region ProductColor
        public async Task<IActionResult> ChangeProductColor(int id)
        {
            ChangeColorViewModel color = await colorService.GetColorByIdAsync(id);
            if (color == null)
            {
                return BadRequest("رنگ محصول یافت نشد");
            }
            return Ok(color);
        }
        #endregion
        #endregion

        #region ProductComment
        #region CreateComment
        [Authorize]
        public async Task<IActionResult> CreateProductComment(int productId)
        {
            CreateProductCommentViewModel model = await commentService.GetProductForCommentAsync(productId);
            return View(model);
        }

        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProductComment(CreateProductCommentViewModel model)
        {
            model.UserId = User.GetUserById();
            #region Validation
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            #endregion
            CreateProductCommentResult result = await commentService.CreateProductCommentAsync(model);
            switch (result)
            {
                case CreateProductCommentResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.AddComment;
                    return RedirectToAction("ProductDetail", "Home", new { slug = model.Slug });
            }
            return View(model);
        }
        #endregion

        #region ReactionComment
        [Authorize]
        public async Task<IActionResult> ReactionComment(ProductReactionCommentViewModel model)
        {
            model.UserId = User.GetUserById();
            await commentService.ProductCommentReactionAsync(model);
            return RedirectToAction("ProductDetail", "Home", new { slug = TempData["Slug"] });

        }
        #endregion
        #endregion

        #region InteresteProduct
        [Authorize]
        public async Task<IActionResult> Interests(int id, string url)
        {
            int userId = User.GetUserById();
            await productService.InterestsAsync(id, userId);
            return Redirect(url);
        }
        #endregion

        #region Error 404
        [Route("error/404")]
        public IActionResult Error404()
        {
            return View();
        }
        #endregion
    }
}
