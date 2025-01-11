using OnlineStore.Domain.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.ContactUs
{
    public class FilterContactUsViewModel:BasePaging<ContactUsViewModel>
    {
        public ContactUsStatus Status { get; set; }
    }

    public enum ContactUsStatus
    {
        [Display(Name ="در انتظار بررسی")]
        AwaitingReview,
        [Display(Name ="پاسخ داده شده")]
        Answer,
        [Display(Name = "همه")]
        All
    

    }
}
