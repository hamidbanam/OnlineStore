using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Context;
using OnlineStore.Domain.InterfaceRepository;
using OnlineStore.Domain.Model.Product.Category;
using OnlineStore.Domain.ViewModel.Admin.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Data.ImplentationRepository
{
    public class CategoryRepository(OnlineStoreContext context) : ICategoryRepository
    {
        public async Task<FilterCategoryViewModel> GetFilterCategoryAsync(FilterCategoryViewModel model)
        {
            var query = context.ProductCategories.Where(c=>!c.IsDelete).AsQueryable();

            #region Filter
            if (!string.IsNullOrEmpty(model.Title))
            {
                query = query.Where(c => c.Title.Contains(model.Title));
            }
            #endregion
            query = query.OrderBy(c => c.ParentId);
            
            await model.Paging(query.Select(c => new CategoryListViewModel()
            {
                CreateDate = c.CreateDate,
                CategoryId = c.CatrgoryId,
                CategoryTitle = c.Title,
                ParentId = c.ParentId,
                ImageCategory=c.ImageCategory,
            }));

            return model;
        }

        public async Task<List<ProductCategory>> GetAllCategoryAsync()
     => await context.ProductCategories.Include(p=>p.Products).ToListAsync();

        public async Task InsertCategoryAsync(ProductCategory category)
      =>await context.ProductCategories.AddAsync(category);

        public async Task SaveAsync()
      =>await context.SaveChangesAsync();

        public async Task UpdateCategoryAsync(EditCategoryViewModel category)
    => await context.ProductCategories.Where(c => c.CatrgoryId == category.CategoryId)
            .ExecuteUpdateAsync(s => s.SetProperty(c => c.Title, category.CategoryTitle)
            .SetProperty(c=>c.ParentId,category.ParentId));

        public async Task DeleteCategoryAsync(int categoryId)
        => await context.ProductCategories.Where(c => c.CatrgoryId == categoryId)
            .ExecuteUpdateAsync(s => s.SetProperty(c => c.IsDelete, true));

        public async Task<ProductCategory> GetCategoryByIdAsync(int categoryId)
      => await context.ProductCategories.FirstOrDefaultAsync(c => c.CatrgoryId == categoryId);

        public void UpdateCategory(ProductCategory category)
       =>context.ProductCategories.Update(category);

 
    }
}
