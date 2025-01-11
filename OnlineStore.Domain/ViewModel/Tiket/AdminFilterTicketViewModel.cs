using OnlineStore.Domain.Enum.Ticket;
using OnlineStore.Domain.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Tiket
{
    public class AdminFilterTicketViewModel:BasePaging<AdminTicketViewModel>
    {
        public TicketStatus Status { get; set; }
    }
}
