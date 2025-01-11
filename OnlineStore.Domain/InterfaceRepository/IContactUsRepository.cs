using OnlineStore.Domain.Model.CountactUs;
using OnlineStore.Domain.Model.Ticket;
using OnlineStore.Domain.ViewModel.ContactUs;
using OnlineStore.Domain.ViewModel.Tiket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.InterfaceRepository
{
    public interface IContactUsRepository
    {
        #region ContactUs
        Task InsertContactUsAsync(ContactUs contactUs);
        Task<FilterContactUsViewModel> FilterContactUsAsync(FilterContactUsViewModel filter);
        Task SaveAsync();
        Task<ContactUs> GetContactUsByIdAsync(int contactusId);
        void UpdateContactUs(ContactUs contactUs);
        Task<int> ContactUsAllCountAsync();
        Task<int> ContactUsPenddingCountAsync();
        Task<int> ContactUsAnswerCountAsync();
        #endregion

        #region Tiket
        Task<List<Ticket>> GetAllTiketByUserIdAsync(int userId);
        Task InsertTiketAsync(Ticket ticket);
        Task InsertTicketMessageAsync(TicketMessage message);
        Task<Ticket> GetTicketByIdAsync(int ticketId,int userId);
        Task<Ticket> GetTicketByIdAsync(int ticketId);
        Task<bool> IsExsistTicketAsync(int  ticketId,int userId);
        Task<bool> IsExsistTicketAsync(int  ticketId);
        Task<AdminFilterTicketViewModel> GetAllTicketAsync(AdminFilterTicketViewModel filter);
        Task UpdateTicketForAdminAnswerAsync(int ticketId);
        Task UpdateTicketForUserAnswerAsync(int ticketId);
        Task UpdateTicketForCloseAnswerAsync(int ticketId);
        Task<int> TicketCountAsync();
        Task<int> TicketCountAnswerAsync();
        Task<int> TicketCountCloseAsync();
        Task<int> TicketCountPenddingAsync();

        #endregion


    }
}
