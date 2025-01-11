using OnlineStore.Domain.ViewModel.UserPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Client.Order
{
    public class AddToOrderViewModel
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int? ColorId { get; set; }
    }

    public enum AddToOrderResult
    {
        Success,
        QuantityInavlid

    }
}
