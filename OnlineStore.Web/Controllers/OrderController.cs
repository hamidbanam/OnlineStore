using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Extensions;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.Model.Order;
using OnlineStore.Domain.Shared;
using OnlineStore.Domain.ViewModel.Client.Order;
using System.Security.Claims;


namespace OnlineStore.Web.Controllers
{
    [Authorize]
    public class OrderController(IOrderService orderService) : SiteBaseController
    {
        #region List
        public async Task<IActionResult> List()
        {
            int userId = User.GetUserById();
            OrderViewModel model = await orderService.GetOrderByUserIdAsync(userId);
      
            return View(model);
        }
        #endregion

        #region Add to Order
        [HttpPost("/basket")]
        public async Task<IActionResult> AddToOrder(AddToOrderViewModel model)
        {
            model.UserId = User.GetUserById();
            #region Validation
            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = WarningMessages.InserField;
                return RedirectToAction(nameof(List), "Order");
            }
            #endregion
            AddToOrderResult result = await orderService.CreateOrderAsync(model);
            switch (result)
            {
                case AddToOrderResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.AddToOrder;
                    return RedirectToAction(nameof(List), "Order");
                case AddToOrderResult.QuantityInavlid:
                    TempData[WarningMessage] = WarningMessages.QuantityPlus;
                    return RedirectToAction(nameof(List), "Order");
            }
            TempData[WarningMessage] = WarningMessages.InserField;
            return RedirectToAction(nameof(List), "Order");
        }
        #endregion

        #region IncreaseProductQuantity
        public async Task<IActionResult> IncreaseProductQuantity(ProductQuantityViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = WarningMessages.InserField;
                return BadRequest(new
                {
                    message = "شناسه ارسال شده صحیح نمی باشد",
                    status = 101
                });
            }
             
            #endregion
            ProductQuantityResult result = await orderService.IncreaseProductQuantity(model);
            switch (result)
            {
                case ProductQuantityResult.Success:
                    return Ok(new
                    {
                        message = "عملیات با موفقیت انجام شد",
                        status = 100
                    });
                case ProductQuantityResult.NotFoundDetail:
                    TempData[WarningMessage] = WarningMessages.QuantityPlus;
                    return BadRequest(new
                    {
                        message = "تعداد درخواست بیش از حد مجاز می باشد",
                        status = 101
                    });
            }
            TempData[WarningMessage] = WarningMessages.InserField;
            return BadRequest(new
            {
                message = "شناسه ارسال شده صحیح نمی باشد",
                status = 101
            });
        }
        #endregion

        #region DecreaseProductQuantity
        public async Task<IActionResult> DecreaseProductQuantity(ProductQuantityViewModel model)
        {
         
            #region Validation
            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = WarningMessages.InserField;
                return BadRequest(new
                {
                    message = "شناسه ارسال شده صحیح نمی باشد",
                    status = 101
                });
            }
            #endregion
            ProductQuantityResult result = await orderService.DecreaseProductQuantityAsync(model);
            switch (result)
            {
                case ProductQuantityResult.Success:
                    return Ok(new
                    {
                        message = "عملیات با موفقیت انجام شد",
                        status = 100
                    });
                default:
                case ProductQuantityResult.NotFoundDetail:
                    TempData[WarningMessage] = WarningMessages.NotFoundData;
                    return BadRequest(new
                    {
                        message = "شناسه ارسال شده صحیح نمی باشد",
                        status = 101
                    });
            }
        }


        #endregion

        #region Delete order detail
        public async Task<IActionResult> DeleteOrderDetails(ProductQuantityViewModel model)
        {

            #region Validation
            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = WarningMessages.InserField;
                return BadRequest(new
                {
                    message = "شناسه ارسال شده صحیح نمی باشد",
                    status = 101
                });
            }
            #endregion
            ProductQuantityResult result = await orderService.DeleteOrderDetailAsync(model);
            switch (result)
            {
                case ProductQuantityResult.Success:
                    return Ok(new
                    {
                        message = "عملیات با موفقیت انجام شد",
                        status = 100
                    });
                default:
                case ProductQuantityResult.NotFoundDetail:
                    TempData[WarningMessage] = WarningMessages.NotFoundData;
                    return BadRequest(new
                    {
                        message = "شناسه ارسال شده صحیح نمی باشد",
                        status = 101
                    });
            }

        }
        #endregion

        #region PayOrder
        public async Task<IActionResult> PayOrder(PayOrderViewModel model)
        {
            model.UserId = User.GetUserById();
            model.Ip = HttpContext.GetUserIp();
            #region Validation
            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = WarningMessages.InserField;
                return RedirectToAction("List", "Order");
            }
            #endregion
            PayOrderResult result = await orderService.PayOrderAsync(model);
            switch (result)
            {
                case PayOrderResult.Success:
                    return RedirectToAction("StartPayByNovino", "Payment", new { area = "", orderId = model.OrderId });
                default:
                case PayOrderResult.NotFoundOrder:
                    TempData[WarningMessage] = WarningMessages.NotFoundData;
                    return RedirectToAction("List", "Order");

            }

        }
        #endregion

        #region Add address to order
        [HttpPost]
          public async  Task AddAddressToOrder(int id)
        {
            int userId=User.GetUserById();
           await orderService.InsertAddressToOrderAsync(id, userId);
        }
        #endregion
    }
}
