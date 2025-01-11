using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Extensions;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.ViewModel.Client.Comment;
using OnlineStore.Domain.ViewModel.Client.Interests;
using OnlineStore.Domain.ViewModel.Client.Order;

namespace OnlineStore.Web.Areas.UserPanel.Controllers
{

    public class HomeController(
        IOrderService orderService,
        IProductService productService,
        IProductCommentService commentService) : UserPanelBaseController
    {
        public async Task<IActionResult> Index()
        {
            int userId = User.GetUserById();
            List<OrderViewModel> model = await orderService.OrderListIsFinalyAsync(userId);
            return View(model);
        }

        #region OrderDetail
        public async Task<IActionResult> OrderDetail(int orderId)
        {
            int userId = User.GetUserById();
            OrderViewModel model = await orderService.GetOrderIsFinalyAsync(userId, orderId);
            return View(model);
        }
        #endregion

        #region Interestes
        #region Intersts list
        public async Task<IActionResult> InterestList(int userId)
        {
            userId = User.GetUserById();
            List<InterestsViewModel> models = await productService.GetInterestsByUserId(userId);
            if (models == null)
            {
                return NotFound();
            }

            return View(models);
        }
        #endregion

        #region DeleteInterestProduct
        public async Task<IActionResult> DeleteInterestProduct(int id)
        {
            await productService.DeleteInterestsAsync(id);
            return Ok(new
            {
                message = "عملیات با موفقیت انجام شد",
                status = 100
            });
        }
        #endregion
        #endregion

        #region Product comment list
        public async Task<IActionResult> CommentList()
        {
            int userId = User.GetUserById();
            List<ClientCommentViewModel> model = await commentService.GetProductCommentsByUserIdAsync(userId);
            return View(model);
        }
        #endregion

    }
}
