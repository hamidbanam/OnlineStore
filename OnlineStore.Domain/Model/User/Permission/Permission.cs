using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Model.User.Permission
{
    public class Permission 
    {
        public int PermissionId { get; set; }
        public string PermissionTitle { get; set; }
        public string PermissionName { get; set; }
        public int? ParentId { get; set; }

        #region Relations
        [ForeignKey(nameof(ParentId))]
        public ICollection<Permission>? Permissions { get; set; }
        public ICollection<RolePermission>? RolePermissions { get; set; }

        #endregion
    }
}
