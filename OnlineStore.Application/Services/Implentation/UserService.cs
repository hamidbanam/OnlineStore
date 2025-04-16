using OnlineStore.Application.Security;
using OnlineStore.Application.Sender.Implentation;
using OnlineStore.Application.Sender.Interface;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.InterfaceRepository;
using OnlineStore.Domain.Model.User.Address;
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

namespace OnlineStore.Application.Services.Implentation
{
    public class UserService(IUserRepository userRepository, ISmsSender smsSender) : IUserService
    {
        public async Task<ChangeMobileViewModel> ChangeMobileAsync(int userId)
        {
            User user = await userRepository.GetUserByIdAsync(userId);
            ChangeMobileViewModel model = new()
            {
                Mobile = user.Mobile,
                UserId = user.UserId,
            };
            return model;
        }

        public async Task<ChangeMobileResult> ChangeMobileAsync(ChangeMobileViewModel model)
        {
            User user = await userRepository.GetUserByIdAsync(model.UserId);
            if (user == null)
            {
                return ChangeMobileResult.NotFound;
            }
            if (model.Mobile != user.Mobile)
            {
                if (await userRepository.IsExsitsMobileAsync(model.Mobile))
                {
                    return ChangeMobileResult.IsMobileDublicated;
                }
            }
            await userRepository.ChangeMobileAsync(model);
            await userRepository.SaveAsync();
            return ChangeMobileResult.Success;
        }

        public async Task<ChangePasswordResult> ChangePasswordAsync(ChangePasswordViewModel model)
        {
            User user = await userRepository.GetUserByIdAsync(model.UserId);
            if (user == null)
            {
                return ChangePasswordResult.NotFound;
            }
            model.Password = SecretHasher.Hash(model.Password);
            await userRepository.ChangePasswordAsync(model);
            await userRepository.SaveAsync();
            return ChangePasswordResult.Success;
        }

        public async Task<ChangeUserNameViewModel> ChangeUserNameAsync(int userId)
        {
            User user = await userRepository.GetUserByIdAsync(userId);
            ChangeUserNameViewModel model = new()
            {
                UserName = user.UserName,
                UserId = userId
            };
            return model;
        }

        public async Task<ChangeUserNameResult> ChangeUserNameAsync(ChangeUserNameViewModel model)
        {
            User user = await userRepository.GetUserByIdAsync(model.UserId);
            if (user == null)
            {
                return ChangeUserNameResult.NotFound;
            }
            if (model.UserName != user.UserName)
            {
                if (await userRepository.IsExsitsUserNameAsync(model.UserName))
                {
                    return ChangeUserNameResult.IsUserNameDublicated;
                }
            }
            await userRepository.ChangeUserNameAsync(model); 
            await userRepository.SaveAsync();
            return ChangeUserNameResult.Success;
        }

        public async Task<bool> CheckPermissionAsync(string permissionTitle, int userId)
       =>await userRepository.CheckPermissionAsync(permissionTitle, userId);

        public async Task<CreateAddressResult> CreateAddressAsync(CreateAddressViewModel model)
        {
            if (model.UserId == null)
            {
                return CreateAddressResult.NotFound;
            }
            Address address = new()
            {
                UserId = model.UserId,
                City = model.City,
                PostCod = model.PostCod,
                State = model.State,
                TotalAddress = model.TotalAddress,
            };
            await userRepository.AddAddressAsync(address);
            await userRepository.SaveAsync();
            return CreateAddressResult.Success;
        }

        public async Task<CreateAdminUserResult> CreateAdminUserAsync(CreateAdminUserViewModel model)
        {

            if (await userRepository.IsExsitsUserNameAsync(model.UserName))
            {
                return CreateAdminUserResult.UserNameDublicated;
            }
            if (await userRepository.IsExsitsEmailAsync(model.Email))
            {
                return CreateAdminUserResult.EmailDublicated;
            }
            if (await userRepository.IsExsitsMobileAsync(model.Mobile))
            {
                return CreateAdminUserResult.ModileDublicated;
            }
            User user = new()
            {
                UserName = model.UserName,
                Mobile = model.Mobile,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                IsActive = model.IsActive,
                IsDelete = false,
                CreateDate = DateTime.Now,
                Password = SecretHasher.Hash(model.Password),
                ConfirmCode = 0
            };
            await userRepository.AddUserAsync(user);
            await userRepository.SaveAsync();

            if (model.RolesId != null)
            {
                await userRepository.AddUserRoleAsync(user.UserId, model.RolesId);
                await userRepository.SaveAsync();
            }
            return CreateAdminUserResult.Success;
        }

        public async Task DeleteAddressAsync(int addressId)
    => await userRepository.DeleteAddressAsync(addressId);

        public async Task DeleteUserAsync(int userId)
     => await userRepository.DeleteUserAsync(userId);

        public async Task<EditAddressViewModel> EditAddressAsync(int addressId)
        {
            Address address = await userRepository.GetAddressByIdAsync(addressId);
            EditAddressViewModel model = new EditAddressViewModel()
            {
                AddressId = address.AddressId,
                City = address.City,
                PostCod = address.PostCod,
                State = address.State,
                TotalAddress = address.TotalAddress,
                UserId=address.UserId
            };
            return model;
        }

        public async Task<EditAddressResult> EditAddressAsync(EditAddressViewModel model)
        {
            Address address = await userRepository.GetAddressByIdAsync(model.AddressId);
            if (address == null)
            {
                return EditAddressResult.NotFound;
            }
            await userRepository.EditAddressAsync(model);
            await userRepository.SaveAsync();
            return EditAddressResult.Success;
        }

        public async Task<EditAdminUserViewModel> EditUserAsync(int userId)
        {
            User user = await userRepository.GetUserByIdAsync(userId);
            EditAdminUserViewModel model = new EditAdminUserViewModel()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsActive = user.IsActive,
                Mobile = user.Mobile,
                RolesId = user.UserRoles.Select(r => r.RoleId).ToList(),
                UserId = userId,
                UserName = user.UserName,
                Roles = await GetAllRolesAsync(),
            };
            return model;
        }

        public async Task<EditUserProfileViewModel> EditUserProfileAsync(int userId)
        {
            User user = await userRepository.GetUserByIdAsync(userId);
            EditUserProfileViewModel model = new()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserId = userId,
            };
            return model;
        }

        public async Task<ForgotPasswordResult> ForgotPasswordAsync(ForgotPasswordViewModel model)
        {
            User user = await userRepository.GetUserByMobileAsync(model.Mobile);
            if (user == null)
            {
                return ForgotPasswordResult.NotFound;
            }
            if (!user.IsActive)
            {
                return ForgotPasswordResult.IsActiveInvalid;
            }
            #region SendSms 
            int sendCode = SecretCode.RandomString();
            if (await userRepository.IsExsitsMobileAsync(model.Mobile))
            {
                string message = "فروشگاه منطقه آزاد انزلی" + "\r\n" +
                    "لطفاً این کد را در اختیار دیگران قرار ندهید" + "\r\n " +
                    $"کد یکبار مصرف: {sendCode}";
                await smsSender.SendSms(user.Mobile, message);
            }
            await userRepository.GetUpdateUserConfirmCodAsync(user.Mobile, sendCode);
            await userRepository.SaveAsync();
            #endregion
            return ForgotPasswordResult.Success;

        }

        public async Task<List<AddressListViewModel>> GetAddressListAsync(int userId)
        {
            return userRepository.GetAddressListByUserIdAsync(userId).Result
                .Select(a => new AddressListViewModel()
                {
                    City = a.City,
                    PostCod = a.PostCod,
                    State = a.State,
                    UserName = a.User.UserName,
                    Mobile = a.User.Mobile,
                    AddressId = a.AddressId,
                    TotalAddress = a.TotalAddress,
                    UserId=userId
                }).ToList();


        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
      => await userRepository.GetAllRolesAsync();

        public async Task<AdminFilterUserViewModel> GetAllUserAsync(AdminFilterUserViewModel model)
      => await userRepository.GetAllUserAsync(model);

        public async Task<User> GetUserByUserNameOrMobile(string userNameOrMobile)
     => await userRepository.GetUserByUserNameOrMobileAsync(userNameOrMobile);

        public async Task<LoginResult> LoginAsync(LoginViewModel model)
        {
            User user = await GetUserByUserNameOrMobile(model.UserNameOrMobile);
            if (user == null)
            {
                return LoginResult.NotFound;
            }
            if (!SecretHasher.Verify(model.Password, user.Password))
            {
                return LoginResult.NotFound;
            }
            if (!user.IsActive)
            {
                return LoginResult.UserIsNotActive;
            }
            return LoginResult.Success;
        }

        public async Task<RegisterResult> RegisterAsync(RegisterViewModel model)
        {
            if (await userRepository.IsExsitsMobileAsync(model.Mobile))
            {
                return RegisterResult.ModileDublicated;
            }
            if (await userRepository.IsExsitsUserNameAsync(model.UserName))
            {
                return RegisterResult.UserNameDublicated;
            }
            User user = new()
            {
                UserName = model.UserName.ToLower().Trim(),
                Mobile = model.Mobile,
                Password = SecretHasher.Hash(model.Password),
                FirstName = null,
                LastName = null,
                Email = null,
                ConfirmCode = null,
                CreateDate = DateTime.Now,
                IsActive = true,
                IsDelete = false,
            };
            await userRepository.AddUserAsync(user);
            await userRepository.SaveAsync();
            return RegisterResult.Success;
        }

        public async Task<ResetPasswordResult> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            User user = await userRepository.GetUserByMobileAndConfirmCodeAsync(model.Mobile, model.ConfirmCode);
            if (user == null)
            {
                return ResetPasswordResult.NotFound;
            }
            string password = SecretHasher.Hash(model.Password);
            await userRepository.GetUpdateUserPasswordAsync(user.Mobile, password);
            await userRepository.SaveAsync();

            return ResetPasswordResult.Success;
        }

        public async Task<EditAdminUserResult> UpdateAdminUserAsync(EditAdminUserViewModel model)
        {
            User user = await userRepository.GetUserByIdAsync(model.UserId);
            if (user == null)
            {
                return EditAdminUserResult.NotFound;
            }
            if (model.UserName != user.UserName)
            {
                if (await userRepository.IsExsitsUserNameAsync(model.UserName))
                {
                    return EditAdminUserResult.UserNameDublicated;
                }
            }
            if (model.Email != user.Email)
            {
                if (await userRepository.IsExsitsEmailAsync(model.Email))
                {
                    return EditAdminUserResult.EmailDublicated;
                }
            }
            if (model.Mobile != user.Mobile)
            {
                if (await userRepository.IsExsitsMobileAsync(model.Mobile))
                {
                    return EditAdminUserResult.ModileDublicated;
                }
            }
            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                user.Password = SecretHasher.Hash(model.Password);
            }
            user.Email = model.Email;
            user.Mobile = model.Mobile;
            user.UserName = model.UserName;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.IsActive = model.IsActive;

            userRepository.UpdateUser(user);
            await userRepository.UpdateUserRoleAsync(user.UserId, model.RolesId);
            await userRepository.SaveAsync();
            return EditAdminUserResult.Success;

        }

        public async Task<EditUserProfileResult> UpdateUserProfileAsync(EditUserProfileViewModel model)
        {
            User user = await userRepository.GetUserByIdAsync(model.UserId);
            if (user == null)
            {
                return EditUserProfileResult.NotFound;
            }
            if (model.Email != user.Email)
            {
                if (await userRepository.IsExsitsEmailAsync(model.Email))
                {
                    return EditUserProfileResult.IsEmailDublicated;
                }
            }
            await userRepository.UpdateUserProfileAsync(model);
            await userRepository.SaveAsync();

            return EditUserProfileResult.Success;
        }
    }
}
