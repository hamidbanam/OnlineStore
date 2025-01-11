using OnlineStore.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Payment
{
    public class StartPaymentViewModel
    {
        public int? WalletId { get; set; }
        public int? OrderId { get; set; }
        public int UserId { get; set; }
        public string? PaymentUrl { get; set; }
    }

    public enum StartPaymentResult
    {
        Success,
        NotFoundWallet,
        NotFoundOrder,
        PaidFaild
    }
}
