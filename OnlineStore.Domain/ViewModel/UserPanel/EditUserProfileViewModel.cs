using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.UserPanel
{
    public class EditUserProfileViewModel
    {
        public int UserId { get; set; }

        [Display(Name = "نام")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر {0} بیش از {1} کارکتر مجاز نمی باشد")]
        [RegularExpression(@"\D+", ErrorMessage = "{0} وارد شده نمی تواند شامل اعداد باشد ")]
        public string? FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر {0} بیش از {1} کارکتر مجاز نمی باشد")]
        [RegularExpression(@"\D+", ErrorMessage = "{0} وارد شده نمی تواند شامل اعداد باشد ")]
        public string? LastName { get; set; }

        [Display(Name = "ایمیل")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر {0} بیش از {1} کارکتر مجاز نمی باشد")]
        [EmailAddress(ErrorMessage = ("فرمت {0} وارد شده صحیح نمی باشد"))]
        public string? Email { get; set; }
    }

    public enum EditUserProfileResult
    {
        Success,
        NotFound,
        IsEmailDublicated
    }
}
