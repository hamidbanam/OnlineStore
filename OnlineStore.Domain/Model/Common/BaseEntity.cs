using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Model.Common
{
    public class BaseEntity
    {
        public DateTime CreateDate { get; set; }= DateTime.Now;
        public bool IsActive { get; set; } = true;
        public bool IsDelete { get; set; }=false;
    }
}
