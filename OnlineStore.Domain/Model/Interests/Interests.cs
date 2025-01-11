using OnlineStore.Domain.Model.Product.Product;
using OnlineStore.Domain.Model.User.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Model.Interests
{
    public class Interests
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime CreateDate { get; set; }

        #region Relations
        [ForeignKey("UserId")]
        public User.User.User User { get; set; }

        [ForeignKey("ProductId")]
        public Product.Product.Product Product { get; set; }
        #endregion
    }

}
