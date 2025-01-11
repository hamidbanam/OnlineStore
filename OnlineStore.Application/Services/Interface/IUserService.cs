using OnlineStore.Domain.Model.User.Role;
using OnlineStore.Domain.Model.User.User;
using OnlineStore.Domain.ViewModel.Account;
using OnlineStore.Domain.ViewModel.Admin.User;
using OnlineStore.Domain.ViewModel.UserPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services.Interface
{ 
    public interface IUserService
    {
        #region Client
        Task<RegisterResult> RegisterAsync(RegisterViewModel model);
        Task<LoginResult> LoginAsync(LoginViewModel model);
        Task<User> GetUserByUserNameOrMobile(string userNameOrMobile);
        Task<ForgotPasswordResult> ForgotPasswordAsync(ForgotPasswordViewModel model);
        Task<ResetPasswordResult> ResetPasswordAsync(ResetPasswordViewModel model);
        Task<EditUserProfileViewModel> EditUserProfileAsync(int userId);
        Task<EditUserProfileResult> UpdateUserProfileAsync(EditUserProfileViewModel model);
        Task<ChangeUserNameViewModel> ChangeUserNameAsync(int userId);
        Task<ChangeUserNameResult> ChangeUserNameAsync(ChangeUserNameViewModel model);
        Task<ChangeMobileViewModel> ChangeMobileAsync(int userId);
        Task<ChangeMobileResult> ChangeMobileAsync(ChangeMobileViewModel model);
        Task<ChangePasswordResult> ChangePasswordAsync(ChangePasswordViewModel model);
        Task<CreateAddressResult> CreateAddressAsync(CreateAddressViewModel model);
        Task<List<AddressListViewModel>> GetAddressListAsync(int userId);
        Task<EditAddressViewModel> EditAddressAsync(int addressId);
        Task<EditAddressResult> EditAddressAsync(EditAddressViewModel model);
        Task DeleteAddressAsync(int addressId);
        Task<bool> CheckPermissionAsync(string permissionTitle, int userId);
        #endregion

        #region Admin
        Task<AdminFilterUserViewModel> GetAllUserAsync(AdminFilterUserViewModel model);
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<CreateAdminUserResult> CreateAdminUserAsync(CreateAdminUserViewModel model);
        Task<EditAdminUserResult> UpdateAdminUserAsync(EditAdminUserViewModel model);
        Task<EditAdminUserViewModel> EditUserAsync(int userId);
        Task DeleteUserAsync(int userId);
        #endregion

    }
}
