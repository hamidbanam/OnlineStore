using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Extensions;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.Model.Wallet;
using OnlineStore.Domain.Shared;
using OnlineStore.Domain.ViewModel.Wallet;
using OnlineStore.Web.Controllers;

namespace OnlineStore.Web.Areas.UserPanel.Controllers
{
   
    public class WalletController(IWalletService walletService) : UserPanelBaseController
    {
        #region List
        [Route("/wallet-list")]
        public async Task<IActionResult> List()
        {
            int userId = User.GetUserById();
            List<WalletViewModel> wallets=await walletService.GetWalletByUserIdAsync(userId);
            return View(wallets);
        }
        #endregion

        #region ChargeWallet
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ChargeWallet(ChargeWalletViewModel model)
        {
            model.UserId = User.GetUserById();
            model.Ip = HttpContext.GetUserIp();
            #region Validation
            if (!ModelState.IsValid)
            {
                TempData[WarningMessage] = WarningMessages.InserField;
                return RedirectToAction("List", "Wallet");
            }
            #endregion
            ChargeWalletResult result = await walletService.ChargeWalletAsync(model);
            switch (result)
            {
                case ChargeWalletResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.ChargeWallet;
                    return RedirectToAction("StartPayByNovino", "Payment", new { area = "", walletId = model.WalletId });
                case ChargeWalletResult.LessAmount:
                    TempData[WarningMessage] = WarningMessages.LessAmount;
                    return RedirectToAction("List", "Wallet");
            }
            return View();
        }
        #endregion


    }
}
