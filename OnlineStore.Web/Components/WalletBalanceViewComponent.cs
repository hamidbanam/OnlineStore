using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Extensions;
using OnlineStore.Application.Services.Implentation;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.Model.Wallet;
using OnlineStore.Domain.ViewModel.Wallet;

namespace OnlineStore.Web.Components
{
    public class WalletBalanceViewComponent(IWalletService walletService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            int userId = UserClaimsPrincipal.GetUserById();
            List<WalletViewModel> wallets = await walletService.GetWalletByUserIdAsync(userId);
            

            return View("WalletBalance", wallets);
        }
    }
}
