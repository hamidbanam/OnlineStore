using OnlineStore.Domain.Enum.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Admin.Comment
{
    public class ProductCommentViewModel
    {
        public int CommentId { get; set; }

        public int ProductId { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string Disadvantages { get; set; }

        public string Advantages { get; set; }

        public DateTime CreateDate { get; set; }

        public ProductCommentStatus Status { get; set; }
    }
}
