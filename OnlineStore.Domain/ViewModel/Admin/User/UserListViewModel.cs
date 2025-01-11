using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Admin.User
{
    public class UserListViewModel
    {
        public int UserId { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر {0} بیش از {1} کارکتر مجاز نمی باشد")]
        public string UserName { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
        [MaxLength(11, ErrorMessage = "تعداد کارکتر {0} بیش از {1} کارکتر مجاز نمی باشد")]
        public string Mobile { get; set; }

        [Display(Name = "تاریخ عضویت")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "وضعیت")]
        public bool IsDelete { get; set; } = false;
    }
}
