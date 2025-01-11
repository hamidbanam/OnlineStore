using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Extensions;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.ViewModel.Client.Order;
using System.Security.Claims;

namespace OnlineStore.Web.Components
{
    public class ShoppingCartViewComponent(IOrderService orderService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            int userId = UserClaimsPrincipal.GetUserById();
            OrderViewModel? model = await orderService.GetOrderByUserIdAsync(userId);
            return View("ShoppingCart", model);
        }
    }
}
