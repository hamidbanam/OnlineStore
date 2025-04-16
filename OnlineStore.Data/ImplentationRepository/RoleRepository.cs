using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Context;
using OnlineStore.Domain.InterfaceRepository;
using OnlineStore.Domain.Model.User.Permission;
using OnlineStore.Domain.Model.User.Role;
using OnlineStore.Domain.ViewModel.Admin.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Data.ImplentationRepository
{
    public class RoleRepository(OnlineStoreContext context) : IRoleRepository
    {
        public async Task AddRolePermissionAsync(RolePermission permission)
        => await context.RolePermissions.AddAsync(permission);

        public async Task AddRoleAsync(Role role)
     => await context.Roles.AddAsync(role);

        public async Task<IEnumerable<Permission>> GetAllPermissionAsync()
     => await context.Permissions.ToListAsync();

        public async Task<IEnumerable<Role>> GetAllRoleAsync()
     => await context.Roles.Where(r=>r.IsDelete==false).ToListAsync();

        public async Task SaveAsync()
      => await context.SaveChangesAsync();

        public async Task<Role?> EditRoleByIdAsync(int roleId)
       => await context.Roles.FirstOrDefaultAsync(r => r.RoleId == roleId);

        public async Task<List<int>> GetRolePermissionAsync(int roleId) 
     => await context.RolePermissions.Where(r => r.RoleId == roleId)
            .Select(p => p.PermissionId).ToListAsync();

        public async Task EditRolePermissionAsync(EditRoleViewModel model)
        {
            await context.Roles.Where(r => r.RoleId == model.RoleId).ExecuteUpdateAsync(setter => setter
            .SetProperty(r => r.RoleTitle, model.RoleTitle));

            await context.RolePermissions.Where(r => r.RoleId == model.RoleId).ExecuteDeleteAsync();
            await AddPermissionToRoleAsync(model.RoleId, model.SelectedPermission);
        }

        public async Task AddPermissionToRoleAsync(int roleId, List<int> permissionId)
        {
            foreach (var item in permissionId)
            {
                await context.RolePermissions.AddAsync(new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = item,
                });
            }
        }

        public async Task DeleteRolePermissionAsync(int roleId)
     =>await context.Roles.Where(r=>r.RoleId== roleId)
            .ExecuteUpdateAsync(setter=>setter
            .SetProperty(r=>r.IsDelete,true));
    }
}
