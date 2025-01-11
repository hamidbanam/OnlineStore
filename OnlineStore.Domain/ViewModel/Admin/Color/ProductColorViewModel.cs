using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Admin.Color
{
    public class ProductColorViewModel
    {
        public int ColorId { get; set; }

        public int ProductId { get; set; }

        public string ProductTitle { get; set; }

        public string Color { get; set; }

        public string? ColorTitle { get; set; }

        public int Price { get; set; }

        public int? Quantity { get; set; }
    }
}
