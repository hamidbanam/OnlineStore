using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Admin.Gallary
{
    public class CreateProductGallaryViewModel
    {
        [Required]
        public int ProductId { get; set; }

        [Display(Name = "عنوان تصویر")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "تعداد کارکتر {0} بیش از {1} کارکتر مجاز نمی باشد")]
        public string ImageTitle { get; set; }

        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
        public IFormFile ImageName { get; set; }
    }

    public enum CreateProductGallaryResult
    {
        Success,
    }
}
