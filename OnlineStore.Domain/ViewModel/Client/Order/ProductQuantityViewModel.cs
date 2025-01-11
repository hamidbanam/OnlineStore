using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Client.Order
{
    public class ProductQuantityViewModel
    {

        public int Id { get; set; }

    }
    public enum ProductQuantityResult
    {
        Success,
        NotFoundDetail
    }
}
