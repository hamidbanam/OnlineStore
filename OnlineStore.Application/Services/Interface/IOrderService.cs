using OnlineStore.Domain.Model.Order;
using OnlineStore.Domain.ViewModel.Client.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services.Interface
{
    public interface IOrderService
    {
        Task<AddToOrderResult> CreateOrderAsync(AddToOrderViewModel model);
        Task<OrderViewModel> GetOrderByUserIdAsync(int userId);
        Task<OrderViewModel> GetOrderIsFinalyAsync(int userId, int orderId);
        Task<List<OrderViewModel>> OrderListIsFinalyAsync(int userId);
        Task<OrderDetail> GetOrderDetailByIdAsync(int detailId);
        Task<ProductQuantityResult> DeleteOrderDetailAsync(ProductQuantityViewModel model);
        Task UpdateOrderDetailAsync(OrderDetail orderDetail);  
        Task<PayOrderResult> PayOrderAsync(PayOrderViewModel model);
        Task<ProductQuantityResult> DecreaseProductQuantityAsync(ProductQuantityViewModel model);
        Task<ProductQuantityResult> IncreaseProductQuantity(ProductQuantityViewModel model);
        Task InsertAddressToOrderAsync(int addressId,int userId);
        Task<int> OrderCountForOneYearAsync();
        Task<int> OrderSumPriceForOneYearAsync();   
        Task<int> OrderCountForOneMonthAsync();
        Task<int> OrderSumPriceForOneMonthAsync();
        Task<List<OrderDetail>> GetAllOrderDetailForJobAsync();
        Task DeleteOrderDetailAsync(int detailId);
    }
}
