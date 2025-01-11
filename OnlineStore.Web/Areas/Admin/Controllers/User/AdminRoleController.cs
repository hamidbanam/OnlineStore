using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Security;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.Shared;
using OnlineStore.Domain.ViewModel.Admin.Role;

namespace OnlineStore.Web.Areas.Admin.Controllers.User
{
    public class AdminRoleController(IRoleService roleService) : AdminBaseController
    {
        #region List
        [PermissionChecker("AdminUser")]
        public async Task<IActionResult> List()
        {
            IEnumerable<RoleViewModel> roles = await roleService.GetAllRolesAsync();
            return View(roles);
        }
        #endregion

        #region Create
        [PermissionChecker("AddUser")]
        public async Task<IActionResult> Create()
        {
            return View(new CreateRoleViewModel()
            {
                Permissions = await roleService.GetAllPermissionAsync(),
            });
        }
        [HttpPost, ValidateAntiForgeryToken, PermissionChecker("AddUser")]
        public async Task<IActionResult> Create(CreateRoleViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
            {
                model.Permissions = await roleService.GetAllPermissionAsync();
                return View(model);
            }

            #endregion
            CreateRoleResult result = await roleService.GetInsertRoleAsync(model);
            switch (result)
            {
                case CreateRoleResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.CreateData;
                    return RedirectToAction("List", "AdminRole");
            }
            model.Permissions = await roleService.GetAllPermissionAsync();
            return View(model);
        }
        #endregion

        #region Edit
        [PermissionChecker("EditUser")]
        public async Task<IActionResult> Edit(int roleId)
        {
            EditRoleViewModel model = await roleService.EditRolePermissionAsync(roleId);
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken, PermissionChecker("EditUser")]
        public async Task<IActionResult> Edit(EditRoleViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
                return View(model);
            #endregion
            EditRoleResult result = await roleService.UpdateRolePermissionAsync(model);
            switch (result)
            {
                case EditRoleResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.UpdateData;
                    return RedirectToAction("List", "AdminRole");
                case EditRoleResult.NotFound:
                    TempData[WarningMessage] = WarningMessages.NotFoundData;
                    return View(model);
            }
            return View(model);
        }
        #endregion

        #region Delete
        [HttpGet, PermissionChecker("DeleteUser")]
        public async Task<IActionResult> Delete(int roleId)
        {
            if (roleId == null)
            {
                return NotFound();
            }
            await roleService.DeleteRolePermissionAsync(roleId);
            return RedirectToAction("List", "AdminRole");
        }
        #endregion
    }
}
