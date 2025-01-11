using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Services.Implentation;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.ViewModel.Admin.Category;

namespace OnlineStore.Web.Components
{
    public class ProductCategoryListViewComponent(ICategoryService categoryService):ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<CategoryListViewModel> model = await categoryService.GetAllCategoryAsync();
            return View("ProductCategoryList", model);
        }
    }
}
