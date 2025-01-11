using OnlineStore.Domain.Model.Common;
using OnlineStore.Domain.Model.Product.Category;
using OnlineStore.Domain.Model.Product.Color;
using OnlineStore.Domain.Model.Product.Comment;
using OnlineStore.Domain.Model.Product.Feature;
using OnlineStore.Domain.Model.Product.Gallary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Model.Product.Product
{
    public class Product:BaseEntity
    {
        [Key]
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public string ProductTitle { get; set; }

        public string TitleForEnglish { get; set; }

        public string Slug { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string Review { get; set; }

        public string? WarningDescription { get; set; }

        public bool IsFreeShipping { get; set; }

        public string? WarrantyText { get; set; }

        public int Quantity { get; set; }

        #region Relations
        [ForeignKey("CategoryId")]
        public ProductCategory? ProductCategory { get; set; }

        public ICollection<ProductGallery>? ProductGalleries { get; set; }

        public ICollection<ProductFeature>? ProductFeatures { get; set; }

        public ICollection<ProductColor>? ProductColors { get; set; }

        public ICollection<Order.OrderDetail>? OrderDetails { get; set; }

        public ICollection<Interests.Interests>? Interests { get; set; }

        public ICollection<ProductComment>? ProductComments { get; set; }
        #endregion

    }
}
