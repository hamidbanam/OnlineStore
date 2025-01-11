using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Extensions;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.Model.Ticket;
using OnlineStore.Domain.ViewModel.Tiket;

namespace OnlineStore.Web.Areas.Admin.Controllers.Ticket
{
    public class AdminTicketController(IContactUsService contactUsService) : AdminBaseController
    {
        #region List
        public async Task<IActionResult> List(AdminFilterTicketViewModel model)
        {
            AdminFilterTicketViewModel ticket = await contactUsService.GetAllTicketAsync(model);
            return View(ticket);
        }
        #endregion

        #region Detail
        public async Task<IActionResult> Detail(int ticketId)
        {
            Domain.Model.Ticket.Ticket ticket = await contactUsService.GetTicketByIdAsync(ticketId);

            if (ticket == null)
                return NotFound();

            return View(ticket);
        }
        #endregion

        #region Answer
        public async Task<IActionResult> Answer(AdminAsnwerTicketViewModel model)
        {
            model.UserId = User.GetUserById();
            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = ModelState.GetModelError();
                return RedirectToAction("Detail", "AdminTicket", new { ticketId = model.TicketId });
            }
            AdminAnswerTicketResult result = await contactUsService.CreateTicketMessageAsync(model);
            switch (result)
            {
                case AdminAnswerTicketResult.Success:
                    return RedirectToAction("Detail", "AdminTicket", new { ticketId = model.TicketId });
                default:
                case AdminAnswerTicketResult.NotFound:
                    TempData[ErrorMessage] = "تیکت مد نظر پیدا نشد";
                    return RedirectToAction("Detail", "AdminTicket", new { ticketId = model.TicketId });
            }
        }
        #endregion

        #region CloseTicket
        public async Task<IActionResult> CloseTicket(int ticketId)
        {
            if(ticketId==null||ticketId==0)
            {
                return NotFound();
            }
            await contactUsService.UpdateTicketForCloseAnswerAsync(ticketId);
            return RedirectToAction("List", "AdminTicket");
        }
        #endregion
    }
}
