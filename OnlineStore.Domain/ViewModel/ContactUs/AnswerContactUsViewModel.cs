using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.ContactUs
{
    public class AnswerContactUsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "پاسخ")]
        [Required(ErrorMessage = "مقدار {0} را وارد نمایید")]
        [MaxLength(800, ErrorMessage = "تعداد کارکتر {0} بیش از {1} کارکتر مجاز نمی باشد")]
        public string Answer { get; set; }
        public int? AnswerUserId { get; set; }

    }

    public enum AnswerContactUsResult
    {
        Success,
        NotFoundContact
    }

}
