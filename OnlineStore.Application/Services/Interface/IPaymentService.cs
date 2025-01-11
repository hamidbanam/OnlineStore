using OnlineStore.Domain.ViewModel.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services.Interface
{
    public interface IPaymentService
    {
        Task<StartPaymentResult> StartPaymentAsync(StartPaymentViewModel model);
        Task<CallBackPaymentResult> CallBackPaymentAsync(CallBackPaymentViewModel model);
    }
}
