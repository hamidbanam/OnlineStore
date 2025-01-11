using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Extensions;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.Shared;
using OnlineStore.Domain.ViewModel.UserPanel;
using OnlineStore.Web.Controllers;

namespace OnlineStore.Web.Areas.UserPanel.Controllers
{

    public class AddressController(IUserService userService) : UserPanelBaseController
    {
        #region Address List
        public async Task<IActionResult> List()
        {
            int userId = User.GetUserById();
            if (userId == null)
            {
                return NotFound();
            }
            List<AddressListViewModel> model = await userService.GetAddressListAsync(userId);
            return View(model);
        }
        #endregion

        #region Create Address
        [HttpGet]
        public async Task<IActionResult> Create(string? url)
        {
            TempData["StringUrl"] = url;
            return View(new CreateAddressViewModel()
            {
                UserId = User.GetUserById(),
            });
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateAddressViewModel model)
        {

            #region Validation
            if (!ModelState.IsValid)
                return View(model);
            #endregion
            CreateAddressResult result = await userService.CreateAddressAsync(model);
            switch (result)
            {
                case CreateAddressResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.CreateData;
                    return TempData["StringUrl"] == null ? RedirectToAction("List", "Address") : Redirect(TempData["StringUrl"].ToString());
                case CreateAddressResult.NotFound:
                    TempData[WarningMessage] = WarningMessages.NotFoundData;
                    return View(model);
            }
            return View(model);
        }
        #endregion

        #region Edit Address
        [HttpGet]
        public async Task<IActionResult> Edit(int addressId)
        {
            if (addressId == null)
            {
                return NotFound();
            }
            EditAddressViewModel model = await userService.EditAddressAsync(addressId);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditAddressViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
                return View(model);
            #endregion
            EditAddressResult result = await userService.EditAddressAsync(model);
            switch (result)
            {
                case EditAddressResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.CreateData;
                    return RedirectToAction("List", "Address");
                case EditAddressResult.NotFound:
                    TempData[WarningMessage] = WarningMessages.NotFoundData;
                    return View(model);
            }
            return View(model);
        }
        #endregion

        #region Delete Address
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await userService.DeleteAddressAsync(id);
            return RedirectToAction("List", "Address");
        }
        #endregion
    }
}
