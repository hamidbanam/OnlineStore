using OnlineStore.Domain.Model.Product.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Client.Interests
{
    public class InterestsViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Model.Product.Product.Product? Product { get; set; }
    }
}
