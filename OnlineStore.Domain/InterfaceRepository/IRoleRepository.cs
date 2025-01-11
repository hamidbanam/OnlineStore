using OnlineStore.Domain.Model.User.Permission;
using OnlineStore.Domain.Model.User.Role;
using OnlineStore.Domain.ViewModel.Admin.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.InterfaceRepository
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllRoleAsync();
        Task<IEnumerable<Permission>> GetAllPermissionAsync();
        Task AddRoleAsync(Role role);
        Task AddRolePermissionAsync(RolePermission permission);
        Task SaveAsync();
        Task<Role> EditRoleByIdAsync(int roleId);
        Task<List<int>> GetRolePermissionAsync(int roleId);
        Task EditRolePermissionAsync(EditRoleViewModel model);
        Task AddPermissionToRoleAsync(int roleId, List<int> permissionId);
        Task DeleteRolePermissionAsync(int roleId);
    }
}
