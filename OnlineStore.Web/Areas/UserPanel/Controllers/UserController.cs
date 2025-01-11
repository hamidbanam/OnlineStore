using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Extensions;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.Shared;
using OnlineStore.Domain.ViewModel.UserPanel;
using OnlineStore.Web.Controllers;

namespace OnlineStore.Web.Areas.UserPanel.Controllers
{

    public class UserController(IUserService userService) : UserPanelBaseController
    {
        #region Edit Profile
        [HttpGet("UserPanel/edit-profile")]
        public async Task<IActionResult> EditProfile()
        {
            EditUserProfileViewModel model = await userService.EditUserProfileAsync(User.GetUserById());
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
        [HttpPost("UserPanel/edit-profile"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(EditUserProfileViewModel model)
        {
            #region Vlidation
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            #endregion
            EditUserProfileResult result = await userService.UpdateUserProfileAsync(model);
            switch (result)
            {
                case EditUserProfileResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.UpdateData;
                    return RedirectToAction("Index", "Home");
                case EditUserProfileResult.NotFound:
                    TempData[WarningMessage] = WarningMessages.NotFoundData;
                    return View(model);
                case EditUserProfileResult.IsEmailDublicated:
                    TempData[WarningMessage] = WarningMessages.DublicatedEmail;
                    return View(model);
            }
            return View(model);
        }
        #endregion

        #region Change UserName
        [HttpGet("UserPanel/change-username")]
        public async Task<IActionResult> ChangeUserName()
        {
            ChangeUserNameViewModel model = await userService.ChangeUserNameAsync(User.GetUserById());
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
        [HttpPost("UserPanel/change-username"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUserName(ChangeUserNameViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
                return View(model);
            #endregion

            ChangeUserNameResult result = await userService.ChangeUserNameAsync(model);
            switch (result)
            {
                case ChangeUserNameResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.ChangeData;
                    return RedirectToAction("Logout", "Account");
                case ChangeUserNameResult.NotFound:
                    TempData[WarningMessage] = WarningMessages.NotFoundData;
                    return View(model);
                case ChangeUserNameResult.IsUserNameDublicated:
                    TempData[WarningMessage] = WarningMessages.DublicatedUserName;
                    return View(model);
            }
            return View(model);
        }
        #endregion

        #region Change Mobile
        [HttpGet("UserPanel/change-mobile")]
        public async Task<IActionResult> ChangeMobile()
        {
            ChangeMobileViewModel model = await userService.ChangeMobileAsync(User.GetUserById());
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
        [HttpPost("UserPanel/change-mobile"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeMobile(ChangeMobileViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
                return View(model);
            #endregion

            ChangeMobileResult result = await userService.ChangeMobileAsync(model);
            switch (result)
            {
                case ChangeMobileResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.ChangeData;
                    return RedirectToAction("Logout", "Account");
                case ChangeMobileResult.NotFound:
                    TempData[WarningMessage] = WarningMessages.NotFoundData;
                    return View(model);
                case ChangeMobileResult.IsMobileDublicated:
                    TempData[WarningMessage] = WarningMessages.DublicatedMobile;
                    return View(model);
            }
            return View(model);
        }
        #endregion

        #region Change Password
        [HttpGet("UserPanel/change-password")]
        public async Task<IActionResult> ChangePassword()
        {
            return View(new ChangePasswordViewModel()
            {
                UserId = User.GetUserById(),
            });
        }
        [HttpPost("UserPanel/change-password"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
                return View(model);
            #endregion

            ChangePasswordResult result = await userService.ChangePasswordAsync(model);
            switch (result)
            {
                case ChangePasswordResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.ChangeData;
                    return RedirectToAction("Logout", "Account");
                case ChangePasswordResult.NotFound:
                    TempData[WarningMessage] = WarningMessages.NotFoundData;
                    return View(model);
            }
            return View(model);
        }
        #endregion

    }
}
