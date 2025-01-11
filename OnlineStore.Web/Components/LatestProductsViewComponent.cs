using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.ViewModel.Client.Product;

namespace OnlineStore.Web.Components
{
    public class LatestProductsViewComponent(IProductService productService):ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<LastesProductViewModel> model=await productService.GetLastesProductsAsync();
            return View("LatestProducts",model);
        }
    }
}
