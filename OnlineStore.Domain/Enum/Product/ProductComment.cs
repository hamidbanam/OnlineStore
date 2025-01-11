using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Enum.Product
{
    public enum ProductCommentStatus
    {
        [Display(Name = "منتظر بررسی")] Pendding=1,

        [Display(Name = "تایید شده")] Conmfirmed=2,

        [Display(Name = "رد شده")] Rejected=3
    }
}
