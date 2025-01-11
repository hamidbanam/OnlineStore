using OnlineStore.Domain.Model.Product.Color;
using OnlineStore.Domain.ViewModel.Admin.Color;
using OnlineStore.Domain.ViewModel.Admin.Product;
using OnlineStore.Domain.ViewModel.Client.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services.Interface
{
    public interface IProductColorService
    {
        Task<List<ProductColorViewModel>> GetProductColorByProductIdAsync(int productId);
        Task<CreateProductColorResult> CreateProductColorAsync(CreateProductColorViewModel model);
        Task DeleteProductColorAsync(int colorId);
        Task<ChangeColorViewModel> GetColorByIdAsync(int colorId);
    }
}
