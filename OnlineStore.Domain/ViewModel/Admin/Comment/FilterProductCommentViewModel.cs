using OnlineStore.Domain.Enum.Product;
using OnlineStore.Domain.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Admin.Comment
{
    public class FilterProductCommentViewModel : BasePaging<ProductCommentViewModel>
    {
        public int? ProductId { get; set; }
        public FilterProductCommentstatus Status { get; set; }
    }


    public enum FilterProductCommentstatus
    {

        [Display(Name = "همه")]
        All,

        [Display(Name = "منتظر بررسی")]
        Pendding,

        [Display(Name = "تایید شده")]
        Conmfirmed,

        [Display(Name = "رد شده")]
        Rejected,


    }
}
