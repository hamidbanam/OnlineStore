using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Slider.HomePageSlider
{
    public class EditHomePageSliderViewModel
    {
        public int SliderId { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفاً مقدار {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر {0} بیش از {1} کارکتر مجاز نیست")]
        public string SliderTitle { get; set; }

        [Display(Name = "تصویر")]
        public string? SliderImage { get; set; }

        [Display(Name = "فایل تصویر")]
        public IFormFile? SliderAvatar { get; set; }

        [Display(Name = "لینک")]
        [Required(ErrorMessage = "لطفاً مقدار {0} را وارد نمایید")]
        public string SliderUrl { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }
    }
    public enum EditHomePageSliderResult
    {
        Success,
        NotFound,

    }
}
