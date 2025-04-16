using Azure.Core;
using Microsoft.AspNetCore.Http;
using OnlineStore.Application.Extensions;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.Enum.Wallet;
using OnlineStore.Domain.InterfaceRepository;
using OnlineStore.Domain.Model.User.User;
using OnlineStore.Domain.Model.Wallet;
using OnlineStore.Domain.ViewModel.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services.Implentation
{
    public class WalletService(IWalletRepository walletRepository) : IWalletService
    {
        public async Task<ChargeWalletResult> AdminChargeWalletAsync(ChargeWalletViewModel model)
        {
            if (model.Price < 5000)
            {
                return ChargeWalletResult.LessAmount;
            }
            Wallet wallet = new()
            {
                Price = model.Price,
                Case = TransactionCase.ChargeWallet,
                CreateDate = DateTime.Now,
                Description = "شارژ کیف پول از طریق ادمین",
                IP = model.Ip,
                OrderId = null,
                Payed = true,
                RefId = null,
                Type = TransactionType.Deposit,
                UserId = model.UserId ?? 0,
            };
            await walletRepository.ChargeWalletAsync(wallet);
            await walletRepository.SaveAsync();
            model.WalletId = wallet.WalletId;
            return ChargeWalletResult.Success;
        }

        public async Task<int> BalanceAmountAsync(int userId)
        {
            int creditorAmount = await walletRepository.CreditorAmountAsunc(userId);
            int dreditorAmount = await walletRepository.DepositAmountAsunc(userId);
            return dreditorAmount-creditorAmount;
        }

        public async Task<ChargeWalletResult> ChargeWalletAsync(ChargeWalletViewModel model)
        {
            if (model.Price < 5000)
            {
                return ChargeWalletResult.LessAmount;
            }
            Wallet wallet = new()
            {
                Price = model.Price,
                Case = TransactionCase.ChargeWallet,
                CreateDate = DateTime.Now,
                Description = "شارژ کیف پول",
                IP = model.Ip,
                OrderId = null,
                Payed = false,
                RefId = null,
                Type = TransactionType.Deposit,
                UserId = model.UserId ?? 0,
            };
            await walletRepository.ChargeWalletAsync(wallet);
            await walletRepository.SaveAsync();
            model.WalletId = wallet.WalletId;
            return ChargeWalletResult.Success; 
        }

        public async Task<List<WalletViewModel>> GetWalletByUserIdAsync(int userId)
        {
            int creditorAmount = await walletRepository.CreditorAmountAsunc(userId);
            int dreditorAmount = await walletRepository.DepositAmountAsunc(userId);
            return (await walletRepository.GetWalletByUserIdAsync(userId))
                .Select(w => new WalletViewModel()
                {
                    WalletId = w.WalletId,
                    UserId = w.UserId,
                    CreateDate = w.CreateDate,
                    Description = w.Description,
                    Payed = w.Payed,
                    Price = w.Price,
                    RefId = w.RefId,
                    Type = w.Type,
                    CreditorAmount = creditorAmount,
                    DepositAmount = dreditorAmount,
                    BalanceAmount = dreditorAmount - creditorAmount,
                }).ToList();
        }
    }
}
