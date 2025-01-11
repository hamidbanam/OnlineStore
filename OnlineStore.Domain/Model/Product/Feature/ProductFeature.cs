using OnlineStore.Domain.Model.Common;
using OnlineStore.Domain.Model.Feature;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Model.Product.Feature
{
    public class ProductFeature : BaseEntity
    {
        [Key]
        public int ProductFeatureId { get; set; }

        public int ProductId { get; set; }

        public int FeatureId { get; set; }

        public string FeatureValue { get; set; }

        public int Order { get; set; }

        #region Relations
        [ForeignKey(nameof(ProductId))]
        public Product.Product? Product { get; set; }

        [ForeignKey(nameof(FeatureId))]
        public Model.Feature.Feature? Feature { get; set; }
        #endregion

    }
}
