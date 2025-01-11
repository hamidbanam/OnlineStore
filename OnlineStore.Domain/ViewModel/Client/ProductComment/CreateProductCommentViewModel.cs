using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Client.ProductComment
{
    public class CreateProductCommentViewModel
    {
        public int ProductId { get; set; }

        public string Slug { get; set; }

        public int UserId { get; set; }

        [Display(Name = "تصویر محصول")]
        public string? ProductImage { get; set; }
        [Display(Name = "عنوان محصول")]
        public string? ProductTitle { get; set; }

        [Display(Name = "عنوان لاتین")]
        public string? ProductEnTitle { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(150, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
        public string Title { get; set; }

        [Display(Name = "متن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(400, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
        public string Text { get; set; }

        [Display(Name = "معایب")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(150, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
        public string Disadvantages { get; set; }

        [Display(Name = "مزیت ها")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(150, ErrorMessage = "تعداد کاراکتر وارد شده بیش از حد مجاز است.")]
        public string Advantages { get; set; }

    }

    public enum CreateProductCommentResult
    {
        Success,
    }
}
