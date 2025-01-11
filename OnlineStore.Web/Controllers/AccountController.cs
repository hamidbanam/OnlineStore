using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Security;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.Model.User.User;
using OnlineStore.Domain.Shared;
using OnlineStore.Domain.ViewModel.Account;
using System.Security.Claims;

namespace OnlineStore.Web.Controllers
{
    public class AccountController(IUserService userService) : SiteBaseController
    {
        #region Register
        [HttpGet("/Register")]
        [LoginRequired]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost("/Register")]
        [ValidateAntiForgeryToken,LoginRequired]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            #region Validations
            if (!ModelState.IsValid)
                return View(model);
            #endregion

            RegisterResult result = await userService.RegisterAsync(model);
            switch (result)
            {
                case RegisterResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.Register;
                    return RedirectToAction("Login", "Account");
                case RegisterResult.ModileDublicated:
                    TempData[WarningMessage] = WarningMessages.DublicatedMobile;
                    return View(model);
                case RegisterResult.UserNameDublicated:
                    TempData[WarningMessage] = WarningMessages.DublicatedUserName;
                    return View(model);
            }
            return View(model);
        }
        #endregion

        #region Login
        [HttpGet("/Login"), LoginRequired]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost("/Login"), LoginRequired]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            #endregion
            LoginResult result = await userService.LoginAsync(model);
            switch (result)
            {
                case LoginResult.Success:
                    Domain.Model.User.User.User user = await userService.GetUserByUserNameOrMobile(model.UserNameOrMobile);
                    if (user == null)
                    {
                        TempData[WarningMessage] = WarningMessages.NotFoundData;
                        return View(model);
                    }
                    #region Login
                    List<Claim> claims = new List<Claim>()
                    {
                     new(ClaimTypes.Name,user.UserName),
                     new(ClaimTypes.MobilePhone,user.Mobile),
                     new(ClaimTypes.NameIdentifier,user.UserId.ToString())
                    };
                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        IsPersistent = true,
                    };

                    await HttpContext.SignInAsync(principal, properties);
                    #endregion
                    TempData[InfoMessage] = InfoMessages.Welcome;
                    return RedirectToAction("Index", "Home");
                case LoginResult.NotFound:
                    TempData[WarningMessage] = WarningMessages.NotFoundData;
                    return View(model);
                case LoginResult.UserIsNotActive:
                    TempData[WarningMessage] = WarningMessages.NotActiveUser;
                    return View(model);
            }
            return View();
        }
        #endregion

        #region Logout
        [HttpGet("/Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region ForgotPassword
        [HttpGet("Forgot-Password"), LoginRequired]
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }
        [HttpPost("Forgot-Password"), ValidateAntiForgeryToken, LoginRequired]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            #region Validation
            if(!ModelState.IsValid)
                return View(model);
            #endregion
            ForgotPasswordResult result=await userService.ForgotPasswordAsync(model);
            switch (result)
            {
                case ForgotPasswordResult.Success:
                    TempData["Mobile"]=model.Mobile;
                    TempData[SuccessMessage]=SuccessMessages.SendCode;
                    return RedirectToAction("ResetPassword", "Account");
                case ForgotPasswordResult.NotFound:
                    TempData[WarningMessage]=WarningMessages.NotFoundData;
                    return View(model);
                case ForgotPasswordResult.IsActiveInvalid:
                    TempData[WarningMessage]=WarningMessages.NotActiveUser;
                    return View(model);
            }
            return View(model);
        }
        #endregion

        #region ResetPassword
        [HttpGet("Reset-Password"), LoginRequired]
        public async Task<IActionResult> ResetPassword()
        {
            string mobile = TempData["Mobile"].ToString();
            if (string.IsNullOrWhiteSpace(mobile))
            {
                return NotFound();
            }
            return View(new ResetPasswordViewModel()
            {
                Mobile = mobile
            });
        }
        [HttpPost("Reset-Password"), ValidateAntiForgeryToken, LoginRequired]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
                return View(model);
            #endregion
            ResetPasswordResult result=await userService.ResetPasswordAsync(model);
            switch (result)
            {
                case ResetPasswordResult.Success:
                    TempData[SuccessMessage]=SuccessMessages.UpdateData;
                    return RedirectToAction("Login", "Account");
                case ResetPasswordResult.NotFound:
                    TempData[WarningMessage]=WarningMessages.NotFoundData;
                    return View(model);
            }
            return View(model);
        }
        #endregion


    }
}
