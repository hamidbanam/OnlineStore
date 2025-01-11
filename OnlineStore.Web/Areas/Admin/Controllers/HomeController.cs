using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Security;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Web.Controllers;

namespace OnlineStore.Web.Areas.Admin.Controllers
{
    public class HomeController(
        IOrderService orderService,
        IProductCommentService commentService,
        IContactUsService contactUsService) : AdminBaseController
    {
        public async Task<IActionResult> Index()
        {
            #region Order
            TempData["OrderCountForYear"] = await orderService.OrderCountForOneYearAsync();
            TempData["OrderSumPriceForYear"] = await orderService.OrderSumPriceForOneYearAsync();
            TempData["OrderCountForMonth"] = await orderService.OrderCountForOneMonthAsync();
            TempData["OrderSumPriceForMonth"] = await orderService.OrderSumPriceForOneMonthAsync();
            #endregion
            #region Comment
            TempData["ProductCommentList"] = await commentService.GetCommentIsPenddingAsync();
            #endregion

            #region Ticket
            TempData["AllTicketCount"] = await contactUsService.TicketCountAsync();
            TempData["PenddingTicketCount"] = await contactUsService.TicketCountPenddingAsync();
            TempData["AnswerTicketCount"] = await contactUsService.TicketCountAnswerAsync();
            TempData["CloseTicketCount"] = await contactUsService.TicketCountCloseAsync();
            #endregion

            #region ContactUs
            TempData["AllConTactUsCount"] = await contactUsService.ContactUsAllCountAsync();
            TempData["AnswerConTactUsCount"] = await contactUsService.ContactUsAnswerCountAsync();
            TempData["PenddingConTactUsCount"] = await contactUsService.ContactUsPenddingCountAsync();
            #endregion

            return View();
        }
    }
}
