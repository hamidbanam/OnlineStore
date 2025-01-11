
using System.ComponentModel.DataAnnotations;


namespace OnlineStore.Domain.ViewModel.Admin.User
{
    public class EditAdminUserViewModel
    {
        public int UserId { get; set; }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر {0} بیش از {1} کارکتر مجاز نمی باشد")]
        public string UserName { get; set; }

        [Display(Name = "موبایل")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
        [MaxLength(11, ErrorMessage = "تعداد کارکتر {0} بیش از {1} کارکتر مجاز نمی باشد")]
        public string Mobile { get; set; }

        [Display(Name = "رمزعبور")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر {0} بیش از {1} کارکتر مجاز نمی باشد")]
        public string? Password { get; set; }

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

        [Display(Name = "نقش کاربر")]
        public List<int>? RolesId { get; set; }

        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }

        public IEnumerable<Model.User.Role.Role>? Roles { get; set; }
    }

    public enum EditAdminUserResult
    {
        Success,
        ModileDublicated,
        UserNameDublicated,
        EmailDublicated,
        NotFound
    }
}
