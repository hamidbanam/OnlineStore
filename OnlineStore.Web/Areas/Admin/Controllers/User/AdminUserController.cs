using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Extensions;
using OnlineStore.Application.Security;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.Shared;
using OnlineStore.Domain.ViewModel.Admin.User;
using OnlineStore.Domain.ViewModel.Wallet;

namespace OnlineStore.Web.Areas.Admin.Controllers.User
{
    public class AdminUserController(
        IUserService userService,
        IWalletService walletService) : AdminBaseController
    {
        #region List
        [PermissionChecker("AdminUser")]
        public async Task<IActionResult> List(AdminFilterUserViewModel filter)
        {
            var model = await userService.GetAllUserAsync(filter);
            return View(model);
        }
        #endregion

        #region Create
        [HttpGet("/Admin/Create-User"), PermissionChecker("AddUser")]
        public async Task<IActionResult> Create()
        {
            return View(new CreateAdminUserViewModel()
            {
                Roles = await userService.GetAllRolesAsync(),
            });
        }
        [HttpPost("/Admin/Create-User"), ValidateAntiForgeryToken, PermissionChecker("AddUser")]
        public async Task<IActionResult> Create(CreateAdminUserViewModel model)
        {
            model.Roles = await userService.GetAllRolesAsync();
            #region Validation
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            #endregion

            CreateAdminUserResult result = await userService.CreateAdminUserAsync(model);
            switch (result)
            {
                case CreateAdminUserResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.CreateData;
                    return RedirectToAction("List", "AdminUser");
                case CreateAdminUserResult.ModileDublicated:
                    TempData[WarningMessage] = WarningMessages.DublicatedMobile;
                    return View(model);
                case CreateAdminUserResult.UserNameDublicated:
                    TempData[WarningMessage] = WarningMessages.DublicatedUserName;
                    return View(model);
                case CreateAdminUserResult.EmailDublicated:
                    TempData[WarningMessage] = WarningMessages.DublicatedEmail;
                    return View(model);
            }

            return View(model);
        }
        #endregion

        #region Edit
        [HttpGet("/Admin/Edit-User"), PermissionChecker("EditUser")]
        public async Task<IActionResult> Edit(int userId)
        {
            EditAdminUserViewModel model = await userService.EditUserAsync(userId);
            return View(model);
        }
        [HttpPost("/Admin/Edit-User"), ValidateAntiForgeryToken, PermissionChecker("EditUser")]
        public async Task<IActionResult> Edit(EditAdminUserViewModel model)
        {
            model.Roles = await userService.GetAllRolesAsync();
            #region Validation
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            #endregion
            EditAdminUserResult result = await userService.UpdateAdminUserAsync(model);
            switch (result)
            {
                case EditAdminUserResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.CreateData;
                    return RedirectToAction("List", "AdminUser");
                case EditAdminUserResult.ModileDublicated:
                    TempData[WarningMessage] = WarningMessages.DublicatedMobile;
                    return View(model);
                case EditAdminUserResult.UserNameDublicated:
                    TempData[WarningMessage] = WarningMessages.DublicatedUserName;
                    return View(model);
                case EditAdminUserResult.EmailDublicated:
                    TempData[WarningMessage] = WarningMessages.DublicatedEmail;
                    return View(model);
                case EditAdminUserResult.NotFound:
                    TempData[WarningMessage] = WarningMessages.NotFoundData;
                    return View(model);
            }
            return View(model);
        }
        #endregion

        #region Delete
        [HttpGet("/Admin/Delete-User"), PermissionChecker("DeleteUser")]
        public async Task<IActionResult> Delete(int userId)
        {
            await userService.DeleteUserAsync(userId);
            return RedirectToAction("List", "AdminUser");
        }
        #endregion

        #region ChargeWallet
        [HttpGet("/Admin/charge-wallet/{userId}"), PermissionChecker("EditUser")]
        public async Task<IActionResult> ChargeWallet(int userId)
        {
            return PartialView(new ChargeWalletViewModel()
            {
                UserId=userId
            });
        }  
        [HttpPost("/Admin/charge-wallet/{userId}"), PermissionChecker("EditUser"),ValidateAntiForgeryToken]
        public async Task<IActionResult> ChargeWallet(ChargeWalletViewModel model)
        {
            model.Ip = $"ادمین با آدی : {User.GetUserById()}";   
            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = WarningMessages.InserField;
                return RedirectToAction("List", "AdminUser");
            }
            ChargeWalletResult result=await walletService.AdminChargeWalletAsync(model);
            switch (result)
            {
                case ChargeWalletResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.ChargeWallet;
                    return RedirectToAction("List", "AdminUser");
                case ChargeWalletResult.LessAmount:
                    TempData[WarningMessage] = WarningMessages.LessAmount;
                    return RedirectToAction("List", "AdminUser");
            }
            TempData[WarningMessage] = WarningMessages.InserField;
            return RedirectToAction("List", "AdminUser");
        }
        #endregion
    }
}
