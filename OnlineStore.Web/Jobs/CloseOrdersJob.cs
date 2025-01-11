using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.Model.Order;
using Quartz;

namespace OnlineStore.Web.Jobs
{
    public class CloseOrdersJob(IOrderService orderService) : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            List<OrderDetail> orderDetail = await orderService.GetAllOrderDetailForJobAsync();
            foreach (var item in orderDetail)
            {
                await orderService.DeleteOrderDetailAsync(item.DetailId);
            }
        }
    }
}
