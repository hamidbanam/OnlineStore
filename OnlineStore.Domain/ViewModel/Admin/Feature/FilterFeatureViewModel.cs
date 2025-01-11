using OnlineStore.Domain.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Admin.Feature
{
    public class FilterFeatureViewModel:BasePaging<FeatureViewModel>
    {
        [Display(Name =("عنوان"))]
        public string? Title { get; set; }
    }
}
