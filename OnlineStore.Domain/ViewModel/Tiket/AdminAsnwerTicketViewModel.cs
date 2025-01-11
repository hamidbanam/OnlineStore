using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Tiket
{
    public class AdminAsnwerTicketViewModel
    {
        public int? UserId { get; set; }
        [Display(Name = "تیکت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public int TicketId { get; set; }

        [Display(Name = "پیام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(1000, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
        public string Message { get; set; }
    }

    public enum AdminAnswerTicketResult
    {
        Success,
        NotFound
    }
}
