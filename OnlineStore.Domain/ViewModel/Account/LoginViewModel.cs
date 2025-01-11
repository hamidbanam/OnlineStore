using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Account
{
    public class LoginViewModel
    {
        [Display(Name = "نام کاربری یا موبایل")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر {0} بیش از {1} کارکتر مجاز نمی باشد")]
        public string UserNameOrMobile { get; set; }

        [Display(Name = "رمزعبور")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر {0} بیش از {1} کارکتر مجاز نمی باشد")]
        public string Password { get; set; }
    }

    public enum LoginResult
    {
        Success,
        NotFound,
        UserIsNotActive
    }
}
