using OnlineStore.Domain.Enum.Product;
using OnlineStore.Domain.Model.User.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Model.Product.Comment
{
    public class ProductComment
    {
        [Key]
        public int CommentId { get; set; }

        public int ProductId { get; set; }

        public int UserId { get; set; }

        [MaxLength(200)]
        public string Title { get; set; }

        public string Text { get; set; }

        public string Disadvantages { get; set; }

        public string Advantages { get; set; }

        public DateTime CreateDate { get; set; }

        public ProductCommentStatus Status { get; set; }

        #region Relations
        [ForeignKey(nameof(ProductId))]
        public Product.Product? Product { get; set; }

        [ForeignKey(nameof(UserId))]
        public User.User.User? User { get; set; }
        #endregion
    }
}
