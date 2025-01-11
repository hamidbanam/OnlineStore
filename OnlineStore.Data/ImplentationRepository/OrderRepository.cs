using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Context;
using OnlineStore.Domain.InterfaceRepository;
using OnlineStore.Domain.Model.Order;
using OnlineStore.Domain.Model.User.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Data.ImplentationRepository
{
    public class OrderRepository(OnlineStoreContext context) : IOrderRepository
    {
        public void DeleteOrder(int orderId)
        {
            Order? order = context.Orders.FirstOrDefault(o => o.OrderId == orderId);
            context.Orders.Remove(order);
            context.SaveChanges();
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            await context.Orders.Where(o => o.OrderId == orderId).ExecuteDeleteAsync();
        }


        public async Task DeleteOrderDetailAsync(int detailId)
  => await context.OrderDetails.Where(od => od.DetailId == detailId).ExecuteDeleteAsync();

        public async Task<Order?> GetOrderByIdAsync(int orderId)
      => await context.Orders.Include(o => o.OrderDetails).FirstOrDefaultAsync(o => o.OrderId == orderId);

        public async Task<Order> GetOrderByUserIdAsync(int userId)
        {
            var order = await context.Orders
              .Include(o => o.OrderDetails)
              .ThenInclude(od => od.Product)
               .Include(o => o.OrderDetails)
             .ThenInclude(od => od.ProductColor)
             .FirstOrDefaultAsync(o => o.UserId == userId && !o.IsFinally);
            return order;
        }

        public async Task<OrderDetail?> GetOrderDetailByIdAsync(int detailId)
      => await context.OrderDetails.Include(od => od.Product).Include(od => od.ProductColor)
            .FirstOrDefaultAsync(od => od.DetailId == detailId);

        public async Task<OrderDetail?> GetOrderDetailByOrderIdAsync(int orderId, int productId, int? colorId)
      => await context.OrderDetails.FirstOrDefaultAsync(or => or.OrderId == orderId && or.ProductId == productId && or.ProductColorId == colorId);

        public async Task<List<Order>> OrderListIsFinalydAsync(int userId)
        {
            var order = await context.Orders
              .Include(o => o.OrderDetails)
              .ThenInclude(od => od.Product)
               .Include(o => o.OrderDetails)
             .ThenInclude(od => od.ProductColor)
             .Where(o => o.UserId == userId && o.IsFinally).ToListAsync();
            return order;
        }

        public async Task InsertAddressToOrderAsync(int addressId, int userId)
      => await context.Orders.Where(o => o.UserId == userId && !o.IsFinally).ExecuteUpdateAsync(
          s => s.SetProperty(o => o.LocationId, addressId));

        public async Task InsertOrderAsync(Order order)
      => await context.Orders.AddAsync(order);

        public async Task InsertOrderDetailAsync(OrderDetail orderDetail)
     => await context.OrderDetails.AddAsync(orderDetail);

        public async Task SaveAsync()
     => await context.SaveChangesAsync();

        public async Task UpdateOrderAsync(int orderId)
     => await context.Orders.Where(o => o.OrderId == orderId).ExecuteUpdateAsync(
         s => s.SetProperty(o => o.IsFinally, true));

        public void UpdateOrderDetail(OrderDetail orderDetail)
    => context.OrderDetails.Update(orderDetail);

        public async Task<Order> GetOrderIsFinalydAsync(int userId, int orderId)
        {
            var order = await context.Orders
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
             .Include(o => o.OrderDetails)
           .ThenInclude(od => od.ProductColor)
           .FirstOrDefaultAsync(o => o.UserId == userId && o.IsFinally && o.OrderId == orderId);
            return order;
        }

        public async Task<List<Order>> OrderListIsFinalyAsync()
     => await context.Orders
              .Include(o => o.OrderDetails)
              .Where(o => o.IsFinally).ToListAsync();

        public async Task<List<OrderDetail>> GetAllOrderDetailForJobAsync()
     =>await context.OrderDetails.Where(od=>od.CreateDate == DateTime.Now.AddMinutes(-30)).ToListAsync();
    }
}

