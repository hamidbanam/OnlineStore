using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Account
{
    public class ForgotPasswordViewModel
    {
        [Display(Name = "موبایل")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
        [MaxLength(11, ErrorMessage = "تعداد کارکتر {0} بیش از {1} کارکتر مجاز نمی باشد")]
        public string Mobile { get; set; }
   
    }

    public enum ForgotPasswordResult
    {
        Success,
        NotFound,
        IsActiveInvalid
    }
}
