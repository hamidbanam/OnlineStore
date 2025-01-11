using OnlineStore.Domain.Model.Common;
using OnlineStore.Domain.Model.User.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Model.User.Role
{
    public class Role:BaseEntity
    {
        public int RoleId { get; set; }
        public string RoleTitle { get; set; }

        #region Relations
        public ICollection<UserRole>? UserRoles { get; set; }
        public ICollection<RolePermission>? RolePermissions { get; set; }
        #endregion

    }
}
