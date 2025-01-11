using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Admin.Gallary
{
    public class ProductGallaryViewModel
    {
        public int GallaryId { get; set; }

        public int ProductId { get; set; }

        [Display(Name = "عنوان تصویر")]
        public string ImageTitle { get; set; }

        [Display(Name = "تصویر")]
        public string ImageName { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
