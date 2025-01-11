using OnlineStore.Domain.Model.User.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Model.User.Address
{
    public class Address
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public double PostCod { get; set; }
        public string TotalAddress { get; set; }

        #region Relations
        [ForeignKey(nameof(UserId))]
        public User.User? User { get; set; }
        #endregion



    }
}
