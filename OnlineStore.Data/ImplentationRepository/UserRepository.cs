using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Context;
using OnlineStore.Domain.InterfaceRepository;
using OnlineStore.Domain.Model.User.Address;
using OnlineStore.Domain.Model.User.Permission;
using OnlineStore.Domain.Model.User.Role;
using OnlineStore.Domain.Model.User.User;
using OnlineStore.Domain.ViewModel.Admin.User;
using OnlineStore.Domain.ViewModel.UserPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Data.ImplentationRepository
{
    public class UserRepository(OnlineStoreContext context) : IUserRepository
    {
        public async Task AddUserAsync(User user)
      => await context.Users.AddAsync(user);

        public async Task<User?> GetUserByMobileAsync(string mobile)
     => await context.Users.FirstOrDefaultAsync(u => u.Mobile == mobile);

        public async Task<User?> GetUserByUserNameOrMobileAsync(string userNameOrMobile)
       => await context.Users.FirstOrDefaultAsync(u => u.UserName == userNameOrMobile || u.Mobile == userNameOrMobile);

        public async Task<bool> IsExsitsMobileAsync(string mobile)
     => await context.Users.AnyAsync(u => u.Mobile == mobile);

        public async Task<bool> IsExsitsUserNameAsync(string userName)
     => await context.Users.AnyAsync(u => u.UserName == userName);

        public async Task SaveAsync()
      => await context.SaveChangesAsync();

        public async Task GetUpdateUserConfirmCodAsync(string mobile, int confirmCode)
        {
            await context.Users.Where(u => u.Mobile == mobile).ExecuteUpdateAsync(setter => setter
            .SetProperty(u => u.ConfirmCode, confirmCode));
        }

        public async Task<User?> GetUserByMobileAndConfirmCodeAsync(string mobile, int confirmCode)
     => await context.Users.FirstOrDefaultAsync(u => u.Mobile == mobile && u.ConfirmCode == confirmCode);

        public async Task GetUpdateUserPasswordAsync(string mobile, string password)
        {
            await context.Users.Where(u => u.Mobile == mobile).ExecuteUpdateAsync(setter => setter
            .SetProperty(u => u.Password, password)
            .SetProperty(u => u.ConfirmCode, 0));
        }

        public async Task<User?> GetUserByIdAsync(int userId)
     => await context.Users.Include(u => u.UserRoles).FirstOrDefaultAsync(u => u.UserId == userId);

        public async Task UpdateUserProfileAsync(EditUserProfileViewModel model)
        {
            await context.Users.Where(u => u.UserId == model.UserId)
                 .ExecuteUpdateAsync(setter => setter
                 .SetProperty(u => u.FirstName, model.FirstName)
                 .SetProperty(u => u.LastName, model.LastName)
                 .SetProperty(u => u.Email, model.Email)
                 );
        }

        public async Task<bool> IsExsitsEmailAsync(string email)
      => await context.Users.AnyAsync(u => u.Email == email);

        public async Task ChangeUserNameAsync(ChangeUserNameViewModel model)
        {
            await context.Users.Where(u => u.UserId == model.UserId)
                 .ExecuteUpdateAsync(setter => setter
                 .SetProperty(u => u.UserName, model.UserName));
        }

        public async Task ChangeMobileAsync(ChangeMobileViewModel model)
     => await context.Users.Where(u => u.UserId == model.UserId)
            .ExecuteUpdateAsync(setter => setter
            .SetProperty(u => u.Mobile, model.Mobile));

        public async Task ChangePasswordAsync(ChangePasswordViewModel model)
     => await context.Users.Where(u => u.UserId == model.UserId)
            .ExecuteUpdateAsync(setter => setter
            .SetProperty(u => u.Password, model.Password));

        public async Task AddAddressAsync(Address address)
       => await context.AddAsync(address);

        public async Task<List<Address>> GetAddressListByUserIdAsync(int userId)
       => await context.Addresses.Include(u => u.User).Where(a => a.UserId == userId).ToListAsync();

        public async Task<Address?> GetAddressByIdAsync(int addressId)
    => await context.Addresses.FirstOrDefaultAsync(a => a.AddressId == addressId);

        public async Task EditAddressAsync(EditAddressViewModel model)
        => await context.Addresses.Where(a => a.AddressId == model.AddressId)
            .ExecuteUpdateAsync(setter => setter
            .SetProperty(a => a.PostCod, model.PostCod)
            .SetProperty(a => a.City, model.City)
            .SetProperty(a => a.State, model.State)
            .SetProperty(a => a.TotalAddress, model.TotalAddress));

        public async Task DeleteAddressAsync(int addressId)
      => await context.Addresses.Where(a => a.AddressId == addressId).ExecuteDeleteAsync();

        public async Task<AdminFilterUserViewModel> GetAllUserAsync(AdminFilterUserViewModel model)
        {
            var query = context.Users.AsQueryable();
            if (!string.IsNullOrEmpty(model.Title))
            {
                query = query.Where(u => u.UserName.Contains(model.Title) || u.Mobile.Contains(model.Title));
            }

            switch (model.Status)
            {
                case FilterUserStatus.NotDeleted:
                    query = query.Where(u => !u.IsDelete);
                    break;
                case FilterUserStatus.All:
                    break;
                case FilterUserStatus.Deleted:
                    query = query.Where(u => u.IsDelete);
                    break;

            }
            query = query.OrderByDescending(u => u.CreateDate);
            await model.Paging(query.Select(u => new UserListViewModel()
            {
                CreateDate = u.CreateDate,
                IsDelete = u.IsDelete,
                IsActive = u.IsActive,
                Mobile = u.Mobile,
                UserId = u.UserId,
                UserName = u.UserName,
            }));
            return model;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
       => await context.Roles.ToListAsync();

        public async Task AddUserRoleAsync(int userId, List<int> roleId)
        {
            foreach (var item in roleId)
            {
                await context.UserRoles.AddAsync(new UserRole()
                {
                    UserId = userId,
                    RoleId = item
                });
            }
        }

        public void UpdateUser(User user)
        {
            context.Users.Update(user);
        }

        public async Task UpdateUserRoleAsync(int userId, List<int> roleId)
        {
            await context.UserRoles.Where(u => u.UserId == userId).ExecuteDeleteAsync();
            await AddUserRoleAsync(userId, roleId);
        }

        public async Task DeleteUserAsync(int userId)
      => await context.Users.Where(u => u.UserId == userId).ExecuteUpdateAsync(
          s => s.SetProperty(u => u.IsDelete, true));

        public async Task<bool> CheckPermissionAsync(string permissionTitle, int userId)
        {
            Permission permission = await context.Permissions.FirstOrDefaultAsync(p => p.PermissionTitle == permissionTitle);

            List<int> userRoles = await context.UserRoles.Where(u => u.UserId == userId)
                .Select(r => r.RoleId).ToListAsync();

            List<int> rolePermission = await context.RolePermissions.Where(p => p.PermissionId == permission.PermissionId)
                .Select(r => r.RoleId).ToListAsync();

            return rolePermission.Any(p => userRoles.Contains(p));
        }
    }
}
