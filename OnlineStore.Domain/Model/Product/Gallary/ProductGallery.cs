using OnlineStore.Domain.Model.Common;
using OnlineStore.Domain.Model.Product.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Model.Product.Gallary
{
    public class ProductGallery:BaseEntity
    {
        [Key]
        public int GallaryId { get; set; }

        public int ProductId { get; set; }

        public string ImageTitle { get; set; }

        public string ImageName { get; set; }

        #region Relations
        [ForeignKey(nameof(ProductId))]
        public Product.Product? Product { get; set; }
        #endregion

    }
}
