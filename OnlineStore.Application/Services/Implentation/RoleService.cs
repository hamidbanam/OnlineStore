using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.InterfaceRepository;
using OnlineStore.Domain.Model.User.Permission;
using OnlineStore.Domain.Model.User.Role;
using OnlineStore.Domain.ViewModel.Admin.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services.Implentation
{
    public class RoleService(IRoleRepository roleRepository) : IRoleService
    {
        public async Task DeleteRolePermissionAsync(int roleId)
        {
            await roleRepository.DeleteRolePermissionAsync(roleId);
            await roleRepository.SaveAsync();
        }

        public async Task<EditRoleViewModel> EditRolePermissionAsync(int roleId) 
        {
            Role role = await roleRepository.EditRoleByIdAsync(roleId);
            EditRoleViewModel model = new()
            {
                RoleId = role.RoleId,
                RoleTitle = role.RoleTitle,
                SelectedPermission = await roleRepository.GetRolePermissionAsync(roleId),
                Permissions = await GetAllPermissionAsync(),
            };
            return model;
        }

        public async Task<IEnumerable<PermissionViewModel>> GetAllPermissionAsync()
       => roleRepository.GetAllPermissionAsync().Result
            .Select(p => new PermissionViewModel()
            {
                ParentId = p.ParentId,
                PermissionId = p.PermissionId,
                PermissionName = p.PermissionName,
                PermissionTitle = p.PermissionTitle,
            }).ToList();


        public async Task<IEnumerable<RoleViewModel>> GetAllRolesAsync()
        => roleRepository.GetAllRoleAsync().Result
            .Select(r => new RoleViewModel()
            {
                RoleId = r.RoleId,
                RoleTitle = r.RoleTitle,
                CreateDate = r.CreateDate,
                IsActive = r.IsActive,
            }).ToList();

        public async Task<CreateRoleResult> GetInsertRoleAsync(CreateRoleViewModel model)
        {
            Role role = new()
            {
                CreateDate = DateTime.Now,
                IsActive = true,
                IsDelete = false,
                RoleTitle = model.RoleTitle,

            };
            await roleRepository.AddRoleAsync(role);
            await roleRepository.SaveAsync();
            await roleRepository.AddPermissionToRoleAsync(role.RoleId, model.SelectedPermission);
            await roleRepository.SaveAsync();
            return CreateRoleResult.Success;
        }

        public async Task<EditRoleResult> UpdateRolePermissionAsync(EditRoleViewModel model)
        {
            if (model == null)
            {
                return EditRoleResult.NotFound;
            }
            await roleRepository.EditRolePermissionAsync(model);
            await roleRepository.SaveAsync();
            return EditRoleResult.Success;
        }
    }
}
