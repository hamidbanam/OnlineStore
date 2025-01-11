using OnlineStore.Domain.Model.Interests;
using OnlineStore.Domain.Model.Product.Product;
using OnlineStore.Domain.ViewModel.Admin.Product;
using OnlineStore.Domain.ViewModel.Client.Interests;
using OnlineStore.Domain.ViewModel.Client.Product;
using OnlineStore.Domain.ViewModel.Client.ProductComment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services.Interface
{
    public interface IProductService
    {
        Task<FilterProductViewModel> GetFilterProductsAsync(FilterProductViewModel model);
        Task<CreateProductResult> CreateProductAsync(CreateProductViewModel model);
        Task<EditProductViewModel> GetEditProductAsync(int productId);
        Task<EditProductResult> UpdateProductAsync(EditProductViewModel model);
        Task<DeleteProductResult> DeleteProductAsync(int productId);
        Task<List<LastesProductViewModel>> GetLastesProductsAsync();
        Task<FilterClientProductViewModel> GetFilterProductsAsync(FilterClientProductViewModel model);
        Task<ProductDetailViewModel> GetProductDetailAsync(string slug);
        Task<List<LastesProductViewModel>> GetProductsByCategoryIdAsync(int categoryId);
        Task InterestsAsync(int productId, int userId); 
        Task<Interests>  GetInterestsAsync(int productId, int userId);
        Task<List<InterestsViewModel>> GetInterestsByUserId(int userId);
        Task DeleteInterestsAsync(int id);

    }
}
