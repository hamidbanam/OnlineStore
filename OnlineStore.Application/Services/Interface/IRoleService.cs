using OnlineStore.Domain.ViewModel.Admin.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services.Interface
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleViewModel>> GetAllRolesAsync();
        Task<IEnumerable<PermissionViewModel>> GetAllPermissionAsync();
        Task<CreateRoleResult> GetInsertRoleAsync(CreateRoleViewModel model);
        Task<EditRoleViewModel> EditRolePermissionAsync(int roleId);
        Task<EditRoleResult> UpdateRolePermissionAsync(EditRoleViewModel model);
        Task DeleteRolePermissionAsync(int roleId);
    }
}
