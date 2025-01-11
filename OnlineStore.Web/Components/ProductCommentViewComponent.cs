using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.ViewModel.Client.Comment;

namespace OnlineStore.Web.Components
{
    public class ProductCommentViewComponent(IProductCommentService commentService):ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int productId)
        {
            List<ClientCommentViewModel> model=await commentService.GetProductCommentsAsync(productId);
            return View("ProductComment",model);
        }
    }
}
