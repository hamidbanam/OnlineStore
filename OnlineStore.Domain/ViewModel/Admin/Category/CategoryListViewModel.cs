using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Admin.Category
{
    public class CategoryListViewModel
    {
        public int CategoryId { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
        [MaxLength(150, ErrorMessage = "تعداد کارکتر {0} بیش از {1} کارکتر مجاز نمی باشد")]
        public string CategoryTitle { get; set; }

        [Display(Name = "دسته بندی والد")]
        public int? ParentId { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "تصویر")]
        public string? ImageCategory { get; set; }
    }

}
