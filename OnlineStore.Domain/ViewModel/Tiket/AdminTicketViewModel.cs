using OnlineStore.Domain.Enum.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Tiket
{
    public class AdminTicketViewModel
    {
        public int TiketId { get; set; }
        public string Title { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public TicketSection Section { get; set; }
        public TicketPriority Priority { get; set; }
        public TicketStatus Status { get; set; }
        public DateTime Createdate  { get; set; }
    }
}
