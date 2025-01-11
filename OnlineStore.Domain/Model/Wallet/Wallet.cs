using OnlineStore.Domain.Enum.Wallet;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Model.Wallet
{
    public class Wallet
    {
        [Key]
        public int WalletId { get; set; }

        public int UserId { get; set; }

        public int? OrderId { get; set; }

        public int Price { get; set; }

        public DateTime CreateDate { get; set; }

        public TransactionType Type { get; set; }

        public TransactionCase Case { get; set; }

        public bool Payed { get; set; }

        public string? IP { get; set; }

        public string? RefId { get; set; }

        public string? Description { get; set; }

        public string? Authority { get; set; }

        #region Relations
        [ForeignKey(nameof(UserId))]
        public User.User.User? User { get; set; }
        
        [ForeignKey(nameof(OrderId))]
        public Order.Order? Order { get; set; }

        #endregion
    }
}
