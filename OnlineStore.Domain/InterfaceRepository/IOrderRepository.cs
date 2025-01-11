using OnlineStore.Domain.Model.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.InterfaceRepository
{
    public interface IOrderRepository
    {
        Task InsertOrderAsync(Order order); 
        Task InsertOrderDetailAsync(OrderDetail orderDetail);
        Task<Order> GetOrderByUserIdAsync(int userId);
        Task<Order> GetOrderIsFinalydAsync(int userId, int orderId);
        Task<List<Order>> OrderListIsFinalydAsync(int userId);
        Task<List<Order>> OrderListIsFinalyAsync();
        Task<OrderDetail> GetOrderDetailByOrderIdAsync(int orderId,int productId, int? colorId);
        Task SaveAsync();
        void UpdateOrderDetail(OrderDetail orderDetail);
        Task<OrderDetail> GetOrderDetailByIdAsync(int detailId);
        Task DeleteOrderDetailAsync(int detailId);
        Task<Order> GetOrderByIdAsync(int orderId); 
        Task UpdateOrderAsync(int orderId);
        Task DeleteOrderAsync(int orderId);
        void DeleteOrder(int orderId);
        Task InsertAddressToOrderAsync(int addressId,int userId);
        Task<List<OrderDetail>> GetAllOrderDetailForJobAsync();
    }
}
