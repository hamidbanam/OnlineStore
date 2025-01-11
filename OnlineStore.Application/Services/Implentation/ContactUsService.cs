using Microsoft.AspNetCore.Http;
using OnlineStore.Application.Sender.Interface;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Application.Static;
using OnlineStore.Domain.Enum.Ticket;
using OnlineStore.Domain.InterfaceRepository;
using OnlineStore.Domain.Model.CountactUs;
using OnlineStore.Domain.Model.Ticket;
using OnlineStore.Domain.Model.User.User;
using OnlineStore.Domain.ViewModel.ContactUs;
using OnlineStore.Domain.ViewModel.Tiket;
using System.Security.Claims;

namespace OnlineStore.Application.Services.Implentation
{
    public class ContactUsService(
        IContactUsRepository contactUsRepository,
        IViewRenderService renderService
        ) : IContactUsService
    {
        public async Task<AnswerContactUsResult> AnswerContactUsAsync(AnswerContactUsViewModel model)
        {
            ContactUs contactUs = await contactUsRepository.GetContactUsByIdAsync(model.Id);
            if (contactUs == null)
            {
                return AnswerContactUsResult.NotFoundContact;
            }
            contactUs.Answer = model.Answer;
            contactUs.AnswerUserId = model.AnswerUserId;

            contactUsRepository.UpdateContactUs(contactUs);
            await contactUsRepository.SaveAsync();
            #region Send email
            string body = await renderService.RenderToStringAsync("_EmailBody", contactUs);
            //            string body = $@"
            //<h3>پاسخ به تماس با ما با عنوان {contactUs.Title} به شرح زیر می باشد.</h3>
            //<p>پاسخ: {contactUs.Answer}</p>";

            SendEmail.Send(contactUs.Email, "فروشگاه منطقه آزاد انزلی", body);
            #endregion
            return AnswerContactUsResult.Success;
        }

        public async Task<int> ContactUsAllCountAsync()
  =>await contactUsRepository.ContactUsAllCountAsync();

        public async Task<int> ContactUsAnswerCountAsync()
    =>await contactUsRepository.ContactUsAnswerCountAsync();

        public async Task<int> ContactUsPenddingCountAsync()
       =>await contactUsRepository.ContactUsPenddingCountAsync();

        public async Task<ContactUsResult> CreateContactUsAsync(CreateContactUsViewModel model)
        {
            ContactUs contactUs = new()
            {
                Answer = null,
                AnswerUserId = null,
                CreateDate = DateTime.Now,
                Description = model.Description,
                Email = model.Email,
                FullName = model.FullName,
                Mobile = model.Mobile,
                Title = model.Title,
                Ip = model.Ip,
            };
            await contactUsRepository.InsertContactUsAsync(contactUs);
            await contactUsRepository.SaveAsync();
            return ContactUsResult.Success;
        }

        public async Task CreateTicketAsync(ClientCreateTicketViewModel model)
        {
            Ticket ticket = new()
            {
                Priority = model.Priority,
                CreateDate = DateTime.Now,
                Section = model.Section,
                Status = TicketStatus.Pending,
                Title = model.Title,
                UserId = model.UserId ?? 0,
            };
            await contactUsRepository.InsertTiketAsync(ticket);
            await contactUsRepository.SaveAsync();

            TicketMessage message = new()
            {
                Message = model.Message,
                SenderId = model.UserId ?? 0,
                TicketId = ticket.TicketId,
                CeateDate=DateTime.Now
            };
            await contactUsRepository.InsertTicketMessageAsync(message);
            await contactUsRepository.SaveAsync();
        }

        public async Task<ClientAsnwerTicketResult> CreateTicketMessageAsync(ClientAsnwerTicketViewModel model)
        {
           if(!await contactUsRepository.IsExsistTicketAsync(model.TicketId,model.UserId??0))
            {
                return ClientAsnwerTicketResult.NotFound;
            }
           await contactUsRepository.UpdateTicketForUserAnswerAsync(model.TicketId);
            TicketMessage message = new()
            {
                CeateDate = DateTime.Now,
                Message = model.Message,
                SenderId=model.UserId ?? 0,
                TicketId=model.TicketId,
            };
            await contactUsRepository.InsertTicketMessageAsync(message);
            await contactUsRepository.SaveAsync();
           return ClientAsnwerTicketResult.Success;
        }

        public async Task<AdminAnswerTicketResult> CreateTicketMessageAsync(AdminAsnwerTicketViewModel model)
        {
            if (!await contactUsRepository.IsExsistTicketAsync(model.TicketId))
            {
                return AdminAnswerTicketResult.NotFound;
            }
     
            await contactUsRepository.UpdateTicketForAdminAnswerAsync(model.TicketId);
            TicketMessage message = new()
            {
                CeateDate = DateTime.Now,
                Message = model.Message,
                SenderId = model.UserId ?? 0,
                TicketId = model.TicketId,
            };
            await contactUsRepository.InsertTicketMessageAsync(message);
            await contactUsRepository.SaveAsync();
            return AdminAnswerTicketResult.Success;
        }

        public async Task<FilterContactUsViewModel> FilterContactUsAsync(FilterContactUsViewModel filter)
      => await contactUsRepository.FilterContactUsAsync(filter);

        public async Task<AdminFilterTicketViewModel> GetAllTicketAsync(AdminFilterTicketViewModel filter)
     =>await contactUsRepository.GetAllTicketAsync(filter);

        public async Task<List<TiketViewModel>> GetAllTiketByUserIdAsync(int userId)
        {
            return (await contactUsRepository.GetAllTiketByUserIdAsync(userId)).Select(t => new TiketViewModel()
            {
                CreateDate = t.CreateDate,
                Priority = t.Priority,
                Section = t.Section,
                Status = t.Status,
                TicketId = t.TicketId,
                Title = t.Title,
            }).ToList();
        }

        public async Task<DetailContactUsViewModel> GetContactUsByIdAsync(int contactusId)
        {
            ContactUs contactUs = await contactUsRepository.GetContactUsByIdAsync(contactusId);
            DetailContactUsViewModel model = new DetailContactUsViewModel()
            {
                Answer = contactUs.Answer,
                AnswerUser = contactUs.AnswerUser,
                ContactId = contactusId,
                Description = contactUs.Description,
                Email = contactUs.Email,
                FullName = contactUs.FullName,
                Mobile = contactUs.Mobile,
                Title = contactUs.Title,
                Ip = contactUs.Ip,
            };
            return model;
        }

        public async Task<Ticket> GetTicketByIdAsync(int ticketId, int userId)
        =>await contactUsRepository.GetTicketByIdAsync(ticketId, userId);

        public async Task<Ticket> GetTicketByIdAsync(int ticketId)
  => await contactUsRepository.GetTicketByIdAsync(ticketId);

        public async Task<int> TicketCountAnswerAsync()
       =>await contactUsRepository.TicketCountAnswerAsync();

        public async Task<int> TicketCountAsync()
      =>await contactUsRepository.TicketCountAsync();

        public async Task<int> TicketCountCloseAsync()
       =>await contactUsRepository.TicketCountCloseAsync();

        public Task<int> TicketCountPenddingAsync()
     =>contactUsRepository.TicketCountPenddingAsync();

        public async Task UpdateTicketForCloseAnswerAsync(int ticketId)
  => await contactUsRepository.UpdateTicketForCloseAnswerAsync(ticketId);
    }
}
