using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Client.Product
{
    public class ChangeColorViewModel
    {
        public int Id { get; set; } 
        public string Price { get; set; }
        public string? ColorName { get; set; }
        public string? ColorTitle { get; set; }
    }
}
