using OnlineStore.Domain.Model.Common;
using OnlineStore.Domain.Model.Product.Feature;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Model.Feature
{
    public class Feature:BaseEntity
    {
        [Key]
        public int FeatureId { get; set; }
        public string Title { get; set; }


        #region Relations
        public ICollection<ProductFeature>? ProductFeatures { get; set; }
        #endregion
    }
}
