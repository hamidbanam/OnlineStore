using Microsoft.AspNetCore.Http;
using OnlineStore.Domain.Model.Product.Category;
using OnlineStore.Domain.ViewModel.Admin.Category;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Admin.Product
{
    public class EditProductViewModel
    {
        public int ProductId { get; set; }

        [Display(Name = "دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public int CategoryId { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(150, ErrorMessage = "تعداد کارکتر {0} بیش از {1} کارکتر مجاز نمی باشد")]
        public string ProductTitle { get; set; }

        [Display(Name = "عنوان لاتین")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(150, ErrorMessage = "تعداد کارکتر {0} بیش از {1} کارکتر مجاز نمی باشد")]
        public string TitleForEnglish { get; set; }

        [Display(Name = "Slug")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(250, ErrorMessage = "تعداد کارکتر {0} بیش از {1} کارکتر مجاز نمی باشد")]
        public string Slug { get; set; }

        [Display(Name = "نقد و بررسی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public string Review { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public int Price { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public string Description { get; set; }

        [Display(Name = "عکس")]
        public IFormFile? NewImage { get; set; }

        public string? ImageName { get; set; }

        [Display(Name = "ارسال رایگان")]
        public bool IsFreeShipping { get; set; }

        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }

        [Display(Name = "متن گارانتی")]
        [MaxLength(700, ErrorMessage = "تعداد کارکتر {0} بیش از {1} کارکتر مجاز نمی باشد")]
        public string? WarrantyText { get; set; }

        [Display(Name = "متن هشدار")]
        public string? WarningDescription { get; set; }

        [Display(Name = "تعداد موجود")]
        public int Quantity { get; set; }

        [Display(Name = "دسته بندی")]
        public List<CategoryListViewModel>? Categories { get; set; }
    }

    public enum EditProductResult
    {
        Success,
        SlugExits,
        NotFound
    }
}
