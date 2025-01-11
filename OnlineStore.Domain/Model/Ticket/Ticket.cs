using OnlineStore.Domain.Enum.Ticket;
using OnlineStore.Domain.Model.User.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Model.Ticket
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }

        public DateTime CreateDate { get; set; }

        public TicketStatus Status { get; set; }

        public TicketSection Section { get; set; }

        public TicketPriority Priority { get; set; }


        #region Relations
        [ForeignKey(nameof(UserId))]
        public User.User.User? User { get; set; }

        public ICollection<TicketMessage>? TicketMessages { get; set; }
        #endregion
    }
}
