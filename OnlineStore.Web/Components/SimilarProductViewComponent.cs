using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.ViewModel.Client.Product;

namespace OnlineStore.Web.Components
{
    public class SimilarProductViewComponent(IProductService productService):ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int CategoryId)
        {
            List<LastesProductViewModel> model=await productService.GetProductsByCategoryIdAsync(CategoryId);
            return View("SimilarProduct",model);
        }
    }
}
