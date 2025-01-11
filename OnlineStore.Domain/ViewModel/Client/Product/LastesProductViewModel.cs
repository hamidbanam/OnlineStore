using OnlineStore.Domain.Model.Product.Category;
using OnlineStore.Domain.Model.Product.Color;
using OnlineStore.Domain.ViewModel.Admin.Category;
using OnlineStore.Domain.ViewModel.Admin.Product;
using OnlineStore.Domain.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Client.Product
{
    public class LastesProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string ProductImage { get; set; }
        public int Price { get; set; }
        public string Slug { get; set; }
        public ICollection<ProductColor> Colors { get; set; }
        public ICollection<ProductCategory> Categories { get; set; }

    }
}
