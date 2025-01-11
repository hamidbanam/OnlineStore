using OnlineStore.Domain.Model.User.Role;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Model.User.Permission
{
    public class RolePermission
    {
        public int RP_Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        #region Relations
        [ForeignKey(nameof(RoleId))]
        public Role.Role? Role { get; set; }

        [ForeignKey(nameof(PermissionId))]
        public Permission? Permission { get; set; }
        #endregion
    }
}
