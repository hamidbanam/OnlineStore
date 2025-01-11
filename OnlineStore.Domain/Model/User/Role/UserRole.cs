using OnlineStore.Domain.Model.User.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Model.User.Role
{
    public class UserRole
    {
        public int UR_Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        #region Relations
        [ForeignKey(nameof(UserId))]
        public User.User? User { get; set; }  

        [ForeignKey(nameof(RoleId))]
        public Role? Role { get; set; }
        #endregion
    }
}
