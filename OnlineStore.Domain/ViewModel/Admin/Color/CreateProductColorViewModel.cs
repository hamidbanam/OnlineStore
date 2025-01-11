using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Admin.Color
{
    public class CreateProductColorViewModel
    {
        [Required]
        public int ProductId { get; set; }

        [Display(Name = "رنگ")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
        public string Color { get; set; }

        [Display(Name = "عنوان رنگ")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر {0} بیش از {1} کارکتر مجاز نمی باشد")]
        public string? ColorTitle { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
        public int Price { get; set; }

        [Display(Name = "تعداد")]
        public int? Quantity { get; set; }
    }
    public enum CreateProductColorResult
    {
        Success,
    }
}
