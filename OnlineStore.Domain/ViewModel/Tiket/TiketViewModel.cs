using OnlineStore.Domain.Enum.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Tiket
{
    public class TiketViewModel
    {
        public int TicketId { get; set; }

        public string Title { get; set; }

        public DateTime CreateDate { get; set; }

        public TicketStatus Status { get; set; }

        public TicketSection Section { get; set; }

        public TicketPriority Priority { get; set; }
    }
}
