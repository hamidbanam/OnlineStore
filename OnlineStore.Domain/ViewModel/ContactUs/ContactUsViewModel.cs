using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.ContactUs
{
    public class ContactUsViewModel
    {
        public int ContactUsId { get; set; }
        public string Title { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTime CreateDate { get; set; }
        public string? Answer { get; set; }

    }
}
