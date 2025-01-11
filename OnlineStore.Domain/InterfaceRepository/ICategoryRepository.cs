using OnlineStore.Domain.Model.Product.Category;
using OnlineStore.Domain.ViewModel.Admin.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.InterfaceRepository
{
    public interface ICategoryRepository
    {
        Task<FilterCategoryViewModel> GetFilterCategoryAsync(FilterCategoryViewModel model);
        Task<List<ProductCategory>> GetAllCategoryAsync();
        Task InsertCategoryAsync(ProductCategory category);
        Task UpdateCategoryAsync(EditCategoryViewModel category);
        void UpdateCategory(ProductCategory category);
        Task DeleteCategoryAsync(int categoryId);
        Task<ProductCategory> GetCategoryByIdAsync(int categoryId);
        Task SaveAsync();
    }
}
