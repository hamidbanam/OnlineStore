using OnlineStore.Domain.Enum.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Wallet
{
    public class WalletViewModel
    {
        public int WalletId { get; set; }
        public int UserId { get; set; }
        public int Price { get; set; }
        public int CreditorAmount { get; set; }
        public int DepositAmount { get; set; }
        public int BalanceAmount { get; set; }
        public DateTime CreateDate { get; set; }
        public TransactionType Type { get; set; }
        public bool Payed { get; set; }
        public string? RefId { get; set; }
        public string? Description { get; set; }
    }
}
