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
    public class TicketMessage
    {
        [Key]
        public int TMId { get; set; }
            
        public int TicketId { get; set; }

        public int SenderId { get; set; }

        public string Message { get; set; }

        public DateTime CeateDate { get; set; }


        #region Relations
        [ForeignKey(nameof(TicketId))]
        public Ticket Ticket { get; set; }   
        
        [ForeignKey(nameof(SenderId))]
        public User.User.User? Sender { get; set; }
        #endregion
    }
}
