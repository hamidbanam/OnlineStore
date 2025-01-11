using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.UserPanel
{
    public class CreateAddressViewModel
    {
        [Required]
        public int UserId { get; set; }

        [Display(Name = "استان")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
        [MaxLength(100, ErrorMessage = "تعداد کارکتر {0} بیش از {1} کارکتر مجاز نمی باشد")]
        public string State { get; set; }

        [Display(Name = "شهر")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
        [MaxLength(100, ErrorMessage = "تعداد کارکتر {0} بیش از {1} کارکتر مجاز نمی باشد")]
        public string City { get; set; }

        [Display(Name = "کدپستی")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
          public double PostCod { get; set; }

        [Display(Name = "آدرس کامل")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
        [MaxLength(1200, ErrorMessage = "تعداد کارکتر {0} بیش از {1} کارکتر مجاز نمی باشد")]
        public string TotalAddress { get; set; }
    }
    public enum CreateAddressResult
    {
        Success,
        NotFound,
    }
}
