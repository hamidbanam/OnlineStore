using OnlineStore.Domain.Enum.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Wallet
{
    public class ChargeWalletViewModel
    {
        public int Price { get; set; }
        public int? UserId { get; set; }
        public string? Ip { get; set; }

        public int? WalletId { get; set; }
    }
     public enum ChargeWalletResult
    {
        Success,
        LessAmount
    }
}
