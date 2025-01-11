using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Admin.ProductFeature
{
    public class ProductFeatureViewModel
    {
        public int ProductFeatureId { get; set; }

        [Display(Name = "محصول")]
        public int ProductId { get; set; }

        [Display(Name = "ویژگی")]
        public int FeatureId { get; set; }

        [Display(Name = "عنوان ویژگی")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر {0} بیش از {1} کارکتر مجاز نمی باشد")]
        public string FeatureValue { get; set; }

       
        public string ProductTitle { get; set; }

     
        public string FeatureTitle { get; set; }

        [Display(Name = "الویت")]
        public int Order { get; set; }
    }
}
