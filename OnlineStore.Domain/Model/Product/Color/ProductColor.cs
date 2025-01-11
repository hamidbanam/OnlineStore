using OnlineStore.Domain.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Model.Product.Color
{
    public class ProductColor:BaseEntity
    {
        [Key]
        public int ColorId { get; set; }

        public int ProductId { get; set; }

        public string Color { get; set; }

        public string? ColorTitle { get; set; }

        public int Price { get; set; }

        public int? Quantity { get; set; }

        #region Relations
        [ForeignKey(nameof(ProductId))]
        public Product.Product? Product { get; set; }

        public ICollection<Order.OrderDetail>? OrderDetails { get; set; }
        #endregion
    }
}
