using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Client.Order
{
    public class PayOrderViewModel
    {
        public int OrderId { get; set; }
        public string? Ip { get; set; }
        public int? UserId { get; set; }

    }

    public enum PayOrderResult
    {
        Success,
        NotFoundOrder,
    }
}
