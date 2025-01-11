using OnlineStore.Domain.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Admin.User
{
    public class AdminFilterUserViewModel:BasePaging<UserListViewModel>
    {
        [Display(Name = "عنوان")]
        public string? Title { get; set; }

        [Display(Name = "وضعیت")]
        public FilterUserStatus Status { get; set; }
    }

    public enum FilterUserStatus
    {
        [Display(Name = "حذف نشده ها")]
        NotDeleted,

        [Display(Name = "همه")]
        All,

        [Display(Name = "حذف شده ها")]
        Deleted,
    }
}
