using OnlineStore.Domain.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Admin.ProductFeature
{
    public class FilterProductFeatureViewModel:BasePaging<ProductFeatureViewModel>
    {
        [Display(Name = ("عنوان"))]
        public string Title { get; set; }
        public int? ProductId { get; set; }
    }
}
