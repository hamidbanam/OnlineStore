using OnlineStore.Domain.Model.Product.Color;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace OnlineStore.Domain.Model.Order
{
    public class OrderDetail
    {
        [Key]
        public int DetailId { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int? ProductColorId { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }

        public DateTime CreateDate { get; set; }

        #region Relations
        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product.Product.Product? Product { get; set; }


        [ForeignKey(nameof(ProductColorId))]
        public ProductColor? ProductColor { get; set; }
        #endregion
    }
}
