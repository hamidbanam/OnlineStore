using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Context;
using OnlineStore.Domain.Enum.Ticket;
using OnlineStore.Domain.InterfaceRepository;
using OnlineStore.Domain.Model.CountactUs;
using OnlineStore.Domain.Model.Ticket;
using OnlineStore.Domain.Model.User.User;
using OnlineStore.Domain.ViewModel.ContactUs;
using OnlineStore.Domain.ViewModel.Tiket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Data.ImplentationRepository
{
    public class ContactUsRepository(OnlineStoreContext context) : IContactUsRepository
    {
        public async Task<int> ContactUsAllCountAsync()
    =>await context.ContactUs.CountAsync();

        public async Task<int> ContactUsAnswerCountAsync()
    =>await context.ContactUs.Where(c=>c.Answer!=null).CountAsync();

        public async Task<int> ContactUsPenddingCountAsync()
      =>await context.ContactUs.Where(c=>c.Answer==null).CountAsync();

        public async Task<FilterContactUsViewModel> FilterContactUsAsync(FilterContactUsViewModel filter)
        {
            var query = context.ContactUs.AsQueryable();
            switch (filter.Status)
            {
                case ContactUsStatus.AwaitingReview:
                    query = query.Where(c => c.Answer == null);
                    break;
                case ContactUsStatus.Answer:
                    query = query.Where(c => c.Answer != null);
                    break;
                case ContactUsStatus.All:
                    break;

            }
            query.OrderByDescending(c => c.CreateDate);
            await filter.Paging(query.Select(c => new ContactUsViewModel()
            {
                ContactUsId = c.ContactId,
                Email = c.Email,
                FullName = c.FullName,
                Mobile = c.Mobile,
                Title = c.Title,
                Answer = c.Answer,
                CreateDate = c.CreateDate,
            }));
            return filter;
        }

        public async Task<AdminFilterTicketViewModel> GetAllTicketAsync(AdminFilterTicketViewModel filter)
        {
            var query = context.Tickets.Include(t => t.User).AsQueryable();
            switch (filter.Status)
            {
                case TicketStatus.Pending:
                    query = query.Where(t => t.Status == TicketStatus.Pending);
                    break;
                case TicketStatus.UserAnswered:
                    query = query.Where(t => t.Status == TicketStatus.UserAnswered);
                    break;
                case TicketStatus.AdminAnswered:
                    query = query.Where(t => t.Status == TicketStatus.AdminAnswered);
                    break;
                case TicketStatus.Close:
                    query = query.Where(t => t.Status == TicketStatus.Close);
                    break;

            }
            query = query.OrderByDescending(q => q.CreateDate);
            await filter.Paging(query.Select(q => new AdminTicketViewModel()
            {
                Createdate = q.CreateDate,
                Email = q.User!.Email,
                FullName = q.User!.FirstName + " " + q.User!.LastName,
                Mobile = q.User!.Mobile,
                Priority = q.Priority,
                Section = q.Section,
                Status = q.Status,
                TiketId = q.TicketId,
                Title = q.Title
            }));
            return filter;
        }

        public async Task<List<Ticket>> GetAllTiketByUserIdAsync(int userId)
      => await context.Tickets.Where(t => t.UserId == userId).ToListAsync();

        public async Task<ContactUs?> GetContactUsByIdAsync(int contactusId)
     => await context.ContactUs.Include(c => c.AnswerUser).FirstOrDefaultAsync(c => c.ContactId == contactusId);

        public async Task<Ticket?> GetTicketByIdAsync(int ticketId, int userId)
     => await context.Tickets.Include(t => t.User).Include(t => t.TicketMessages)
            .ThenInclude(t => t.Sender).FirstOrDefaultAsync(t => t.TicketId == ticketId && t.UserId == userId);

        public async Task<Ticket?> GetTicketByIdAsync(int ticketId)
   => await context.Tickets.Include(t => t.User).Include(t => t.TicketMessages)
            .ThenInclude(t => t.Sender).FirstOrDefaultAsync(t => t.TicketId == ticketId);

        public async Task InsertContactUsAsync(ContactUs contactUs)
       => await context.ContactUs.AddAsync(contactUs);

        public async Task InsertTicketMessageAsync(TicketMessage message)
     => await context.TicketMessages.AddAsync(message);

        public async Task InsertTiketAsync(Ticket ticket)
     => await context.Tickets.AddAsync(ticket);

        public async Task<bool> IsExsistTicketAsync(int ticketId, int userId)
        => await context.Tickets.AnyAsync(t => t.TicketId == ticketId && t.UserId == userId);

        public async Task<bool> IsExsistTicketAsync(int ticketId)
 => await context.Tickets.AnyAsync(t => t.TicketId == ticketId);

        public async Task SaveAsync()
      => await context.SaveChangesAsync();

        public async Task<int> TicketCountAnswerAsync()
        => await context.Tickets.Where(t => t.Status == TicketStatus.AdminAnswered).CountAsync();

        public async Task<int> TicketCountAsync()
      => await context.Tickets.CountAsync();

        public async Task<int> TicketCountCloseAsync()
       => await context.Tickets.Where(t => t.Status == TicketStatus.Close).CountAsync();

        public async Task<int> TicketCountPenddingAsync()
     => await context.Tickets.Where(t => t.Status == TicketStatus.Pending).CountAsync();

        public void UpdateContactUs(ContactUs contactUs)
       => context.ContactUs.Update(contactUs);

        public async Task UpdateTicketForAdminAnswerAsync(int ticketId)
    => await context.Tickets.Where(t => t.TicketId == ticketId)
            .ExecuteUpdateAsync(s => s.SetProperty(t => t.Status, TicketStatus.AdminAnswered));

        public async Task UpdateTicketForCloseAnswerAsync(int ticketId)
   => await context.Tickets.Where(t => t.TicketId == ticketId)
            .ExecuteUpdateAsync(s => s.SetProperty(t => t.Status, TicketStatus.Close));

        public async Task UpdateTicketForUserAnswerAsync(int ticketId)
    => await context.Tickets.Where(t => t.TicketId == ticketId)
            .ExecuteUpdateAsync(s => s.SetProperty(t => t.Status, TicketStatus.UserAnswered));
    }
}
