using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Account
{
    public class ResetPasswordViewModel
    {
        public string Mobile { get; set; }

        [Display(Name = "کد ارسالی")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
        public int ConfirmCode { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر {0} بیش از حد  مجاز می باشد")]
        public string Password { get; set; }

        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر {0} بیش از حد  مجاز می باشد")]
        [Compare("Password", ErrorMessage = "رمز عبور و تکرار آن مغایرت دارند")]
        public string RePassword { get; set; }
    }

    public  enum ResetPasswordResult
    {
        Success,
        NotFound,
    }
}
