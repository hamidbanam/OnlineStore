using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Admin.Product
{
    public class ProductViewModel
    {
        public int ProductId { get; set; } 

        public int CategoryId { get; set; }

        public string Title { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
