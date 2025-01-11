using Eshop.Domain.ViewModels.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Extensions;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.ViewModel.Payment;

namespace OnlineStore.Web.Controllers
{
 
    public class PaymentController(IPaymentService paymentService) : SiteBaseController
    {
        [Authorize]
        public async Task<IActionResult> StartPayByNovino(StartPaymentViewModel model)
        {
            model.UserId = User.GetUserById();
            #region Validation
            if (!ModelState.IsValid)
            {
                return View("ErrorPayment", new ErrorPaymentViewModel()
                {
                    Message = "خطایی رخ داده است. لطفا از طریق تیکت به پشتیبانی اعلام کنید"
                });
            }
            #endregion
            StartPaymentResult result = await paymentService.StartPaymentAsync(model);
            switch (result)
            {
                case StartPaymentResult.Success:
                    return Redirect(model.PaymentUrl);
                case StartPaymentResult.NotFoundWallet:
                    return View("ErrorPayment", new ErrorPaymentViewModel()
                    {
                        Message = "خطایی رخ داده است. لطفا از طریق تیکت به پشتیبانی اعلام کنید"
                    });
                case StartPaymentResult.NotFoundOrder:
                    return View("ErrorPayment", new ErrorPaymentViewModel()
                    {
                        Message = "خطایی رخ داده است. لطفا از طریق تیکت به پشتیبانی اعلام کنید"
                    });
                default:
                case StartPaymentResult.PaidFaild:
                    return View("ErrorPayment", new ErrorPaymentViewModel()
                    {
                        Message = "خطایی رخ داده است. لطفا از طریق تیکت به پشتیبانی اعلام کنید"
                    });
            }


        }

        [HttpPost]
        public async Task<IActionResult> NovinoCallback(CallBackPaymentViewModel model)
        {
            #region Validation
            if (!ModelState.IsValid)
            {
                return View("ErrorPayment", new ErrorPaymentViewModel()
                {
                    Message = "خطایی رخ داده است. لطفا از طریق تیکت به پشتیبانی اعلام کنید"
                });

            }
            #endregion

            CallBackPaymentResult result = await paymentService.CallBackPaymentAsync(model);
            switch (result)
            {
                case CallBackPaymentResult.SuccessPayment:
                    return View("SuccessPayment", new SuccessPaymentViewModel()
                    {
                        Message = "پرداخت شما با موفقیت انجام شد.",
                        RefId = model.RefId
                    });
                case CallBackPaymentResult.ErrorPayment:
                    return View("ErrorPayment", new ErrorPaymentViewModel()
                    {
                        Message = "خطایی رخ داده است. لطفا از طریق تیکت به پشتیبانی اعلام کنید."
                    });
                default:
                case CallBackPaymentResult.NotFoundWallet:
                    return View("ErrorPayment", new ErrorPaymentViewModel()
                    {
                        Message = "خطایی رخ داده است. لطفا از طریق تیکت به پشتیبانی اعلام کنید."
                    });
            }

        }
    }
}
