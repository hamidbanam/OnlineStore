using OnlineStore.Domain.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Admin.Product
{
    public class FilterProductViewModel:BasePaging<ProductViewModel>
    {
        [Display(Name = "عنوان")]
        public string? Title { get; set; }

        [Display(Name = "قیمت")]
        public int? Price { get; set; }

        [Display(Name = "وضعیت")]
        public FilterProductStatus Status { get; set; }
    }

    public enum FilterProductStatus
    {
        [Display(Name = "حذف نشده ها")]
        NotDeleted,

        [Display(Name = "همه")]
        All,

        [Display(Name = "حذف شده ها")]
        Deleted,
    }
}
