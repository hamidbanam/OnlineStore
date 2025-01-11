using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Extensions;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.Model.Ticket;
using OnlineStore.Domain.Shared;
using OnlineStore.Domain.ViewModel.Tiket;

namespace OnlineStore.Web.Areas.UserPanel.Controllers
{
    public class TiketController(IContactUsService contactUsService) : UserPanelBaseController
    {
        #region List
        public async Task<IActionResult> List()
        {
            int userId = User.GetUserById();
            List<TiketViewModel> model = await contactUsService.GetAllTiketByUserIdAsync(userId);
            return View(model);
        }
        #endregion

        #region Create
        [HttpGet("/ticket-create")]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost("/ticket-create"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientCreateTicketViewModel model)
        {
            model.UserId = User.GetUserById();
            #region Validation
            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = WarningMessages.InserField;
                return View(model);
            }
            #endregion
            await contactUsService.CreateTicketAsync(model);
            TempData[SuccessMessage] = SuccessMessages.CreateData;
            return RedirectToAction("List", "Tiket");
        }
        #endregion

        #region Detail
        public async Task<IActionResult> Detail(int ticketId)
        {
            int currentUserId = User.GetUserById();
            Ticket ticket = await contactUsService.GetTicketByIdAsync(ticketId, currentUserId);

            if (ticket == null)
                return NotFound();

            return View(ticket);
        }
        #endregion

        #region Answer
        [HttpPost]
        public async Task<IActionResult> Answer(ClientAsnwerTicketViewModel model)
        {
            model.UserId = User.GetUserById();
            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = ModelState.GetModelError();
                return RedirectToAction("Detail", "Tiket", new { ticketId = model.TicketId });
            }
            ClientAsnwerTicketResult result = await contactUsService.CreateTicketMessageAsync(model);
            switch (result)
            {
                case ClientAsnwerTicketResult.Success:
                    return RedirectToAction("Detail", "Tiket", new { ticketId = model.TicketId });
                default:
                case ClientAsnwerTicketResult.NotFound:
                    TempData[ErrorMessage] = "تیکت مد نظر پیدا نشد";
                    return RedirectToAction("Detail", "Tiket", new { ticketId = model.TicketId });
            }

        }
        #endregion
    }
}
