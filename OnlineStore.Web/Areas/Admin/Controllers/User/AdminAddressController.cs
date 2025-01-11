using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Extensions;
using OnlineStore.Application.Security;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.Shared;
using OnlineStore.Domain.ViewModel.UserPanel;

namespace OnlineStore.Web.Areas.Admin.Controllers.User
{
    public class AdminAddressController(IUserService userService) : AdminBaseController
    {
        #region List
        [PermissionChecker("AdminUser")]
        public async Task<IActionResult> List(int userId)
        {
            ViewData["UserId"] = userId;
            List<AddressListViewModel> model = await userService.GetAddressListAsync(userId);
            return View(model);
        }
        #endregion

        #region Create Address
        [HttpGet("/Admin/Create-Address/{userId}"), PermissionChecker("AddUser")]
        public async Task<IActionResult> Create(int userId)
        {
            return PartialView(new CreateAddressViewModel()
            {
                UserId = userId,
            });
        }
        [HttpPost("/Admin/Create-Address/{userId}"), PermissionChecker("AddUser"),ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAddressViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = WarningMessages.InserField;
                return RedirectToAction("List", "AdminAddress", new { userId = model.UserId });
            }

            #endregion
            CreateAddressResult result = await userService.CreateAddressAsync(model);
            switch (result)
            {
                case CreateAddressResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.CreateData;
                    return RedirectToAction("List", "AdminAddress", new { userId = model.UserId });
                case CreateAddressResult.NotFound:
                    TempData[WarningMessage] = WarningMessages.NotFoundData;
                    return RedirectToAction("List", "AdminAddress", new { userId = model.UserId });
            }
            TempData[ErrorMessage] = ErrorMessages.OprationFaild;
            return RedirectToAction("List", "AdminAddress", new { userId = model.UserId });
        }
        #endregion

        #region Edit Address
        [HttpGet("/Admin/Edit-Address/{addressId}"), PermissionChecker("EditUser")]
        public async Task<IActionResult> Edit(int addressId)
        {
            if (addressId == null)
            {
                return NotFound();
            }
            EditAddressViewModel model = await userService.EditAddressAsync(addressId);
            return PartialView(model);
        }

        [HttpPost("/Admin/Edit-Address/{addressId}"), PermissionChecker("EditUser"),ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditAddressViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = WarningMessages.InserField;
                return RedirectToAction("List", "AdminAddress", new { userId = model.UserId });
            }

            #endregion
            EditAddressResult result = await userService.EditAddressAsync(model);
            switch (result)
            {
                case EditAddressResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.CreateData;
                    return RedirectToAction("List", "AdminAddress", new { userId = model.UserId });
                case EditAddressResult.NotFound:
                    TempData[WarningMessage] = WarningMessages.NotFoundData;
                    return RedirectToAction("List", "AdminAddress", new { userId = model.UserId });
            }
            return RedirectToAction("List", "AdminAddress", new { userId = model.UserId });
        }
        #endregion

        #region Delete Address
        [HttpGet, PermissionChecker("DeleteUser")]
        public async Task<IActionResult> Delete(int id, int userId)
        {
            if (id == null)
            {
                return NotFound();
            }
            await userService.DeleteAddressAsync(id);
            return RedirectToAction("List", "AdminAddress", new { userId });
        }
        #endregion

    }
}
