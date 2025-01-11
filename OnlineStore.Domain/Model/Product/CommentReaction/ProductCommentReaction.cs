using OnlineStore.Domain.Enum.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Model.Product.CommentReaction
{
    public class ProductCommentReaction
    {
        [Key]
        public int ReactionId { get; set; }

        public int ProductId { get; set; }

        public int CommentId { get; set; }

        public int UserId { get; set; }

        public DateTime CreateDate { get; set; }

        public ProductCommentReactionType Type { get; set; }

      
    }
}
