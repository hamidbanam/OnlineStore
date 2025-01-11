using OnlineStore.Domain.Model.Product.Category;
using OnlineStore.Domain.Model.Product.Color;
using OnlineStore.Domain.Model.Product.Feature;
using OnlineStore.Domain.Model.Product.Gallary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Client.Product
{
    public class ProductDetailViewModel
    {
        [Display(Name = "آدی")]
        public int ProductId { get; set; }

        [Display(Name = "دسته بندی")]
        public int CategoryId { get; set; }

        [Display(Name = "عنوان محصول")]
        public string ProductTitle { get; set; }

        [Display(Name = "عنوان لاتین")]
        public string TitleForEnglish { get; set; }

        [Display(Name = "عنوان در مرورگر")]
        public string Slug { get; set; }

        [Display(Name = "قیمت")]
        public int Price { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "عکس")]
        public string Image { get; set; }

        [Display(Name = "نقد و بررسی")]
        public string Review { get; set; }

        [Display(Name = "متن هشدار")]
        public string? WarningDescription { get; set; }

        [Display(Name = "ارسال رایگان")]
        public bool IsFreeShipping { get; set; }

        [Display(Name = "متن گارانتی")]
        public string? WarrantyText { get; set; }

        [Display(Name = "تعداد")]
        public int Quantity { get; set; }

        public ICollection<ProductColor> Colors { get; set; }
        public ICollection<ProductGallery> Galleries { get; set; }
        public ICollection<ProductFeature> Features { get; set; }
        public ProductCategory? Category { get; set; }
    }
}
