using OnlineStore.Domain.ViewModel.Admin.Feature;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Admin.ProductFeature
{
    public class EditProductFeatureViewModel
    {
        public int ProductFeatureId { get; set; }

        [Display(Name = "محصول")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
        public int ProductId { get; set; }

        [Display(Name = "ویژگی")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
        public int FeatureId { get; set; }

        public List<FeatureViewModel>? Feature { get; set; }

        [Display(Name = "عنوان ویژگی")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر {0} بیش از {1} کارکتر مجاز نمی باشد")]
        public string FeatureValue { get; set; }

        [Display(Name = "الویت")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
        public int Order { get; set; }
    }

    public enum EditProductFeatureResult
    {
        Success,
        NotFound
    }
}
