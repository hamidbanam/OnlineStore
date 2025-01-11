using OnlineStore.Domain.Enum.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Client.ProductComment
{
    public class ProductReactionCommentViewModel
    {
        public int ProductId { get; set; }

        public int CommentId { get; set; }

        public int UserId { get; set; }

        public ProductCommentReactionType Type { get; set; }
    }
}
