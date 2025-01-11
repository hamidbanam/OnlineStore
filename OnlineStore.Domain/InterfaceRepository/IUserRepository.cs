using OnlineStore.Domain.Model.User.Address;
using OnlineStore.Domain.Model.User.Role;
using OnlineStore.Domain.Model.User.User;
using OnlineStore.Domain.ViewModel.Admin.User;
using OnlineStore.Domain.ViewModel.UserPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.InterfaceRepository
{
    public interface IUserRepository
    {
        #region Client
        Task AddUserAsync(User user);
        Task<bool> IsExsitsMobileAsync(string mobile);
        Task<bool> IsExsitsUserNameAsync(string userName);
        Task<bool> IsExsitsEmailAsync(string email);
        Task SaveAsync();
        Task<User> GetUserByUserNameOrMobileAsync(string userNameOrMobile);
        Task<User> GetUserByMobileAsync(string mobile);
        Task GetUpdateUserConfirmCodAsync(string mobile, int confirmCode);
        Task<User> GetUserByMobileAndConfirmCodeAsync(string mobile, int confirmCode);
        Task GetUpdateUserPasswordAsync(string mobile, string password);
        Task<User> GetUserByIdAsync(int userId);
        Task UpdateUserProfileAsync(EditUserProfileViewModel model);
        Task ChangeUserNameAsync(ChangeUserNameViewModel model);
        Task ChangeMobileAsync(ChangeMobileViewModel model);
        Task ChangePasswordAsync(ChangePasswordViewModel model);
        Task AddAddressAsync(Address address);
        Task<List<Address>> GetAddressListByUserIdAsync(int userId);
        Task<Address> GetAddressByIdAsync(int addressId);
        Task EditAddressAsync(EditAddressViewModel model);
        Task DeleteAddressAsync(int addressId);
        Task<bool> CheckPermissionAsync(string permissionTitle, int userId);
        #endregion

        #region Admin
        Task<AdminFilterUserViewModel> GetAllUserAsync(AdminFilterUserViewModel model);
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task AddUserRoleAsync(int userId, List<int> roleId);
        void UpdateUser(User user);
        Task UpdateUserRoleAsync(int userId, List<int> roleId);
        Task DeleteUserAsync(int userId);
        #endregion
    }
}
