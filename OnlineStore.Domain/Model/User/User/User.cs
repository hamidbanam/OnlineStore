using OnlineStore.Domain.Model.Common;
using OnlineStore.Domain.Model.Interests;
using OnlineStore.Domain.Model.Order;
using OnlineStore.Domain.Model.Product.Comment;
using OnlineStore.Domain.Model.Ticket;
using OnlineStore.Domain.Model.User.Address;
using OnlineStore.Domain.Model.User.Role;
using OnlineStore.Domain.Model.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Model.User.User
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }
        public string UserName { get; set; } 
        public string Mobile { get; set; }
        public string Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public int? ConfirmCode { get; set; }


        #region Relations
        public ICollection<Address.Address>? Addresses { get; set; }
        public ICollection<UserRole>? UserRoles { get; set; }
        public ICollection<Order.Order>? Orders { get; set; }
        public ICollection<ProductComment>? ProductComments { get; set; }
        public ICollection<Ticket.Ticket>? Tickets { get; set; }
        public ICollection<Ticket.TicketMessage>? TicketMessages { get; set; }
        public ICollection<Wallet.Wallet>? Wallets { get; set; }
        public ICollection<CountactUs.ContactUs>? AnswerContactUs { get; set; }
        public ICollection<Interests.Interests>? Interests { get; set; }
        #endregion
    }
}
