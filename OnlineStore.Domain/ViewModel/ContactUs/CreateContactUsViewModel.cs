using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.ContactUs
{
    public class CreateContactUsViewModel
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر {0} بیش از {1} کارکتر مجاز نمی باشد")]
        public string Title { get; set; }

        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر {0} بیش از {1} کارکتر مجاز نمی باشد")]
        public string FullName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر {0} بیش از {1} کارکتر مجاز نمی باشد")]
        public string Email { get; set; }

        [Display(Name = "موبایل")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
        [MaxLength(15, ErrorMessage = "تعداد کارکتر {0} بیش از {1} کارکتر مجاز نمی باشد")]
        public string Mobile { get; set; }

        [Display(Name = "پیام")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
        [MaxLength(800, ErrorMessage = "تعداد کارکتر {0} بیش از {1} کارکتر مجاز نمی باشد")]
        public string Description { get; set; }

        public string? Ip { get; set; }


    }

    public enum ContactUsResult
    {
        Success,
    }
}
