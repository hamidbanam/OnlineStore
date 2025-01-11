using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Admin.Comment
{
    public class ConfirmOrRejectCommentViewModel
    {
        public int CommentId { get; set; }
        public int ProductId { get; set; }
    }
    public enum ConfirmOrRejectCommentResult
    {
        Success,
        NotFound
    }
}
