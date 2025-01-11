using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.UserPanel
{
    public class AddressListViewModel
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public double PostCod { get; set; }
        public string Mobile { get; set; }
        public string? TotalAddress { get; set; }

    }
}
