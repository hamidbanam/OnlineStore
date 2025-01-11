using OnlineStore.Domain.Model.Common;
using OnlineStore.Domain.Model.Product.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Model.Product.Category
{
    public class ProductCategory:BaseEntity
    {
        [Key]
        public int CatrgoryId { get; set; }

        public string Title { get; set; }

        public int? ParentId { get; set; }

        public string? ImageCategory { get; set; }

        #region Relations
        [ForeignKey(nameof(ParentId))]
        public ProductCategory? Category { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }

        public ICollection<Product.Product> Products { get; set; }

        #endregion
    }
}
