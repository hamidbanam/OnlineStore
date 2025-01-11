using OnlineStore.Domain.Model.User.Permission;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Admin.Role
{
    public class CreateRoleViewModel
    {

        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر {0} بیش از {1} کارکتر مجاز نمی باشد")]
        [RegularExpression(@"\D+", ErrorMessage = "{0} وارد شده نمی تواند شامل اعداد باشد ")]
        public string RoleTitle { get; set; }

        public IEnumerable<PermissionViewModel>? Permissions { get; set; }

        public List<int>? SelectedPermission { get; set; }
    }

    public enum CreateRoleResult
    {
        Success,
    }
}
