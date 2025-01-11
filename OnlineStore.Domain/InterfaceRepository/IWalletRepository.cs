using OnlineStore.Domain.Enum.Wallet;
using OnlineStore.Domain.Model.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.InterfaceRepository
{
    public interface IWalletRepository
    {
        Task ChargeWalletAsync(Wallet wallet);
        Task SaveAsync();
        Task<List<Wallet>> GetWalletByUserIdAsync(int userId);
        Task<int> CreditorAmountAsunc(int userId);
        Task<int> DepositAmountAsunc(int userId);
        Task<Wallet> GetWalletByIdAsync(int walletId);
        Task<Wallet> GetWalletByOrderIdAsync(int orderId);
        void UpdateWallet(Wallet wallet);
        
    }
}
