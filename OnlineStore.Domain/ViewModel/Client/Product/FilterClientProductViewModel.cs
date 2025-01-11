using OnlineStore.Domain.Model.Product.Category;
using OnlineStore.Domain.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Client.Product
{
    public class FilterClientProductViewModel : BasePaging<LastesProductViewModel>
    {
        public string? Tilte { get; set; }

        public int? Price { get; set; }

        public int? CategoryId { get; set; }

    }
}
