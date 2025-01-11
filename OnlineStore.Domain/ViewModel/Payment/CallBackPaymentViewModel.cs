using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Payment
{
    public class CallBackPaymentViewModel
    {
        public string PaymentStatus { get; set; }
        public string Authority { get; set; }
        public string InvoiceId { get; set; }
        public int? OrderId { get; set; }
        public int? WalletId { get; set; }
        public string? RefId { get; set; }
    }

    public enum CallBackPaymentResult
    {
        SuccessPayment,
        ErrorPayment,
        NotFoundWallet
    }
}
