using OnlineStore.Domain.ViewModel.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services.Interface
{
    public interface IWalletService
    {
        Task<ChargeWalletResult> ChargeWalletAsync(ChargeWalletViewModel model);
        Task<ChargeWalletResult> AdminChargeWalletAsync(ChargeWalletViewModel model);
        Task<List<WalletViewModel>> GetWalletByUserIdAsync(int userId);
        Task<int> BalanceAmountAsync(int userId);


    }
}
