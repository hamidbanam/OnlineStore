using OnlineStore.Domain.Model.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Client.Order
{
    public class OrderViewModel
    {
        public int? OrderId { get; set; }

        public int UserId { get; set; }

        public bool? IsFinally { get; set; }

        public DateTime CreateDate { get; set; }

        public string?  RefId { get; set; }

        public int? Price { get; set; }

        public ICollection<OrderDetail>? orderDetails { get; set; }

    }

}
