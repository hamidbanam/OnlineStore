using OnlineStore.Domain.Model.Ticket;
using OnlineStore.Domain.ViewModel.ContactUs;
using OnlineStore.Domain.ViewModel.Tiket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services.Interface
{
    public interface IContactUsService
    {
        #region ContactUs
        Task<ContactUsResult> CreateContactUsAsync(CreateContactUsViewModel model);
        Task<FilterContactUsViewModel> FilterContactUsAsync(FilterContactUsViewModel filter);
        Task<DetailContactUsViewModel> GetContactUsByIdAsync(int contactusId);
        Task<AnswerContactUsResult> AnswerContactUsAsync(AnswerContactUsViewModel model);
        Task<int> ContactUsAllCountAsync();
        Task<int> ContactUsPenddingCountAsync();
        Task<int> ContactUsAnswerCountAsync();
        #endregion

        #region Tiket
        Task<List<TiketViewModel>> GetAllTiketByUserIdAsync(int userId);
        Task CreateTicketAsync(ClientCreateTicketViewModel model);
        Task<Ticket> GetTicketByIdAsync(int ticketId, int userId);
        Task<Ticket> GetTicketByIdAsync(int ticketId);
        Task<ClientAsnwerTicketResult> CreateTicketMessageAsync(ClientAsnwerTicketViewModel model);
        Task<AdminAnswerTicketResult> CreateTicketMessageAsync(AdminAsnwerTicketViewModel model);
        Task<AdminFilterTicketViewModel> GetAllTicketAsync(AdminFilterTicketViewModel filter);
        Task UpdateTicketForCloseAnswerAsync(int ticketId);
        Task<int> TicketCountAsync();
        Task<int> TicketCountAnswerAsync();
        Task<int> TicketCountCloseAsync();
        Task<int> TicketCountPenddingAsync();

        #endregion
    }
}
