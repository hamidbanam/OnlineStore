using OnlineStore.Domain.Model.Product.Product;
using OnlineStore.Domain.Model.Wallet;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Model.Order
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public int LocationId { get; set; }

        public bool IsFinally { get; set; }

        public DateTime CreateDate { get; set; }


        #region Relations
        [ForeignKey(nameof(UserId))]
        public User.User.User? User { get; set; }

        public ICollection<OrderDetail>? OrderDetails { get; set; }
        public ICollection<Wallet.Wallet>? Wallets { get; set; }

        #endregion
    }
}
