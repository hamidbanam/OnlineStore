using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.DTOs;
using OnlineStore.Domain.Enum.Wallet;
using OnlineStore.Domain.InterfaceRepository;
using OnlineStore.Domain.Model.Order;
using OnlineStore.Domain.Model.User.User;
using OnlineStore.Domain.Model.Wallet;
using OnlineStore.Domain.ViewModel.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services.Implentation
{
    public class PaymentService(
        IWalletRepository walletRepository,
        IWalletService walletService,
        IOrderRepository orderRepository,
        IUserRepository userRepository,
        INovinoService novinoService) : IPaymentService
    {
        public async Task<CallBackPaymentResult> CallBackPaymentAsync(CallBackPaymentViewModel model)
        {
            if (model.PaymentStatus.ToLower() == "ok")
            {
                string correctAuthority = "";
                int price = 0;
                if (model.WalletId.HasValue)
                {
                    Wallet wallet = await walletRepository.GetWalletByIdAsync(model.WalletId.Value);
                    if (wallet == null)
                    {
                        return CallBackPaymentResult.NotFoundWallet;
                    }
                    price = wallet.Price * 10;
                    correctAuthority = wallet.Authority;
                }
                else if (model.OrderId.HasValue)
                {
                    Wallet wallet = await walletRepository.GetWalletByOrderIdAsync(model.OrderId.Value);
                    if (wallet != null)
                    {
                        price = wallet.Price * 10;
                        correctAuthority = wallet.Authority;
                    }
                }

                var result = await novinoService.VerifyAsync(new NovinoVerifyPaymentRequestDto()
                {
                    Amount = price,
                    Authority = correctAuthority,
                    MerchantId = "test"
                });
                if (result.Status != "100")
                {
                    return CallBackPaymentResult.ErrorPayment;
                }

                if (model.WalletId.HasValue)
                {
                    Wallet wallet = await walletRepository.GetWalletByIdAsync(model.WalletId.Value);
                    wallet.Payed = true;
                    wallet.RefId = result.Data.RefId;
                    walletRepository.UpdateWallet(wallet);
                    await walletRepository.SaveAsync();

                }
                else if (model.OrderId.HasValue)
                {
                    Order order = await orderRepository.GetOrderByIdAsync(model.OrderId.Value);
                    Wallet wallet = await walletRepository.GetWalletByOrderIdAsync(model.OrderId.Value);
                    #region Order
                    await orderRepository.UpdateOrderAsync(model.OrderId.Value);
                    #endregion

                    #region Wallet
                    wallet.Payed = true;
                    wallet.RefId = result.Data.RefId;
                    walletRepository.UpdateWallet(wallet);
                    await walletRepository.SaveAsync();
                    #endregion

                    #region Add Creditor Wallet
                    Wallet creditorWallet = new()
                    {
                        OrderId = order.OrderId,
                        Case = TransactionCase.BuyProduct,
                        CreateDate = DateTime.Now,
                        Description = "خرید محصول ",
                        IP=wallet.IP,
                        Payed = true,
                        Price=wallet.Price,
                        RefId=null,
                        Type=TransactionType.Creditor,
                        UserId = wallet.UserId,
                        
                    };
                    await walletRepository.ChargeWalletAsync(creditorWallet);
                    await walletRepository.SaveAsync();
                    #endregion
                }
                model.RefId = result.Data.RefId;
            }
            else
            {
                return CallBackPaymentResult.ErrorPayment;
            }
            return CallBackPaymentResult.SuccessPayment;
        }

        public async Task<StartPaymentResult> StartPaymentAsync(StartPaymentViewModel model)
        {
            Wallet? wallet = null;
            Order? order = null;
            int price = 0;
            string invoiceId = "";
            string description = "";
            if (model.WalletId.HasValue)
            {
                wallet = await walletRepository.GetWalletByIdAsync(model.WalletId.Value);
                if (wallet == null)
                {
                    return StartPaymentResult.NotFoundWallet;
                }
                price = wallet.Price * 10;
                invoiceId = $"wallet_{wallet.WalletId}";
                description = "شارژ کیف پول";
            }
            if (model.OrderId.HasValue)
            {
          
                order = await orderRepository.GetOrderByIdAsync(model.OrderId.Value);
                if (order == null)
                {
                    return StartPaymentResult.NotFoundOrder;
                }
                price = order.OrderDetails.Sum(od => od.Price * od.Quantity)* 10;
                invoiceId = $"order_{order.OrderId}";
                description = "خرید محصول از فروشگاه منطقه آزاد";
            }
            User user = await userRepository.GetUserByIdAsync(model.UserId);
            #region Send request to payment
            var result = await novinoService.CreateRequestAsync(new NovinoGetPaymentUrlRequestDto()
            {
                MerchantId = "test",
                Amount = price,
                CallBackMethod = "POST",
                CallBackUrl = $"https://localhost:7149/payment/NovinoCallback?walletId={wallet?.WalletId}&orderId={order?.OrderId}",
                Description = description,
                Email = user.Email ?? "",
                Mobile = user.Mobile,
                InvoiceId = invoiceId,
                Name = $"{user.FirstName} {user.LastName}" ?? user.UserName,
            });
            #endregion
            if (result.Status != "100")
            {
                return StartPaymentResult.PaidFaild;
            }
            if (wallet != null)
            {
                wallet.Authority = result.Data.Authority;
                walletRepository.UpdateWallet(wallet);
                await walletRepository.SaveAsync();
            }
            else if (order != null)
            {
                wallet = await walletRepository.GetWalletByOrderIdAsync(order.OrderId);
                if (wallet != null)
                {
                    wallet.Authority = result.Data.Authority;
                    walletRepository.UpdateWallet(wallet);
                    await walletRepository.SaveAsync();
                }
            }
            model.PaymentUrl = result.Data.PaymentUrl;
            return StartPaymentResult.Success;
        }
    }
}
