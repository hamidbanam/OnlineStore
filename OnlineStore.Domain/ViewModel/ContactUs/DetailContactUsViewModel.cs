using OnlineStore.Domain.Model.User.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.ContactUs
{
    public class DetailContactUsViewModel
    {
        public int ContactId { get; set; }
        [Display(Name ="عنوان")]
        public string Title { get; set; }

        [Display(Name ="نام و نام خانوادگی")]
        public string FullName { get; set; }

        [Display(Name ="ایمیل")]
        public string Email { get; set; }

        [Display(Name ="موبایل")]
        public string Mobile { get; set; }

        [Display(Name ="پیام")]
        public string Description { get; set; }

        [Display(Name ="پاسخ")]
        public string? Answer { get; set; }

        [Display(Name ="پاسخ دهنده")]
        public User? AnswerUser { get; set; }

        [Display(Name ="آی پی")]
        public string Ip { get; set; }
    }
}
