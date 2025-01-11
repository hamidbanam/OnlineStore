using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.Shared;
using OnlineStore.Domain.ViewModel.Admin.Comment;
using System;

namespace OnlineStore.Web.Areas.Admin.Controllers.Product
{
    public class AdminProductCommentController(IProductCommentService commentService) : AdminBaseController
    {
        #region List
        public async Task<IActionResult> List(FilterProductCommentViewModel filter)
        {
            if (filter.ProductId == 0 || filter.ProductId == null)
            {
                return NotFound();
            }
            TempData["ProductId"] = filter.ProductId;
            FilterProductCommentViewModel model = await commentService.FilterProductCommentAsync(filter);
            return View(model);
        }
        #endregion

        #region ConfirmComment
        public async Task<IActionResult> ConfirmComment(ConfirmOrRejectCommentViewModel model, string? url)
        {
            if (url == null)
                model.ProductId = (int)TempData["ProductId"];
            #region Validation
            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = WarningMessages.InserField;
                return RedirectToAction("List", new { productId = model.ProductId });
            }
            #endregion
            ConfirmOrRejectCommentResult result = await commentService.ConfirmCommentAsync(model);
            switch (result)
            {
                case ConfirmOrRejectCommentResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.ChangeData;
                    return url == null ? RedirectToAction("List", "AdminProduct", new { productId = model.ProductId }) : Redirect(url);
                default:
                case ConfirmOrRejectCommentResult.NotFound:
                    TempData[WarningMessage] = WarningMessages.NotFoundData;
                    return RedirectToAction("List", new { productId = model.ProductId });

            }
        }
        #endregion   

        #region RejectComment
        public async Task<IActionResult> RejectComment(ConfirmOrRejectCommentViewModel model, string? url)
        {
            if (url == null)
                model.ProductId = (int)TempData["ProductId"];
            #region Validation
            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = WarningMessages.InserField;
                return RedirectToAction("List", new { productId = model.ProductId });
            }
            #endregion
            ConfirmOrRejectCommentResult result = await commentService.RejectCommentAsync(model);
            switch (result)
            {
                case ConfirmOrRejectCommentResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.ChangeData;
                    return url == null ? RedirectToAction("List", "AdminProduct", new { productId = model.ProductId }) : Redirect(url);
                default:
                case ConfirmOrRejectCommentResult.NotFound:
                    TempData[WarningMessage] = WarningMessages.NotFoundData;
                    return RedirectToAction("List", new { productId = model.ProductId });

            }
        }
        #endregion

    }
}
