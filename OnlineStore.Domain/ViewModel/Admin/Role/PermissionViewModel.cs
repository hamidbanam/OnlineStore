using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Admin.Role
{
    public class PermissionViewModel
    {
        public int PermissionId { get; set; }
        public string PermissionTitle { get; set; }
        public string PermissionName { get; set; }
        public int? ParentId { get; set; }
    }
}
