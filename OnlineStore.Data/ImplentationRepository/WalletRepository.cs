using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Context;
using OnlineStore.Domain.Enum.Wallet;
using OnlineStore.Domain.InterfaceRepository;
using OnlineStore.Domain.Model.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Data.ImplentationRepository
{
    public class WalletRepository(OnlineStoreContext context) : IWalletRepository
    {
        public async Task ChargeWalletAsync(Wallet wallet) 
      => await context.Wallets.AddAsync(wallet);

        public async Task<int> CreditorAmountAsunc(int userId)
        => await context.Wallets.Where(w => w.UserId == userId && w.Payed && w.Type == TransactionType.Creditor).SumAsync(w=>w.Price);

        public async Task<int> DepositAmountAsunc(int userId)
        => await context.Wallets.Where(w => w.UserId == userId && w.Payed && w.Type == TransactionType.Deposit).SumAsync(w => w.Price);

        public async Task<Wallet?> GetWalletByIdAsync(int walletId)
=> await context.Wallets.FirstOrDefaultAsync(w => w.WalletId == walletId);

        public async Task<Wallet?> GetWalletByOrderIdAsync(int orderId)
        => await context.Wallets.FirstOrDefaultAsync(w => w.OrderId == orderId);

        public async Task<List<Wallet>> GetWalletByUserIdAsync(int userId)
      =>await context.Wallets.Where(w=>w.UserId==userId).OrderByDescending(w=>w.CreateDate).ToListAsync(); 

        public async Task SaveAsync()
     =>await context.SaveChangesAsync();

        public void UpdateWallet(Wallet wallet)
     =>context.Wallets.Update(wallet);
    }
}
