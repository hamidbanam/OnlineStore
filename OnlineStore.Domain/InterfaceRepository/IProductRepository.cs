using OnlineStore.Domain.Model.Interests;
using OnlineStore.Domain.Model.Product.Comment;
using OnlineStore.Domain.Model.Product.Product;
using OnlineStore.Domain.ViewModel.Admin.Product;
using OnlineStore.Domain.ViewModel.Client.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.InterfaceRepository
{
    public interface IProductRepository
    {
        Task<FilterProductViewModel> GetFilterProductsAsync(FilterProductViewModel model);
        Task<List<Product>> GetLastesProductsAsync();
        Task InsertProductAsync(Product product);
        Task SaveAsync();
        Task<bool> IsExistSlug(string slug);
        Task<bool> IsExistProductAsync(int productId);
        Task<Product> GetProductByIdAsync(int ProductId);
        void EditProduct(Product product);
        Task DeleteProductAsync(int poductId);
        Task UndoDeleteProductAysnc(int productId);
        Task<FilterClientProductViewModel> GetFilterProductsAsync(FilterClientProductViewModel model);
        Task<Product> GetProductBySlugAsync(string slug);
        Task<List<Product>> GetProductListByCategoryIdAsync(int categoryId);
        Task InsertInterestsAsync(Interests interests);
        Task DeleteInterestsAsync(int id);
        Task<Interests> GetInterestsAsync(int productId, int userId);
        Task<List<Interests>> GetInterestsByUserIdAsync(int userId);
      

    }
}
