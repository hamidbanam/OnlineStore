using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Context;
using OnlineStore.Domain.InterfaceRepository;
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

namespace OnlineStore.Data.ImplentationRepository
{
    public class ProductRepository(OnlineStoreContext context) : IProductRepository
    {
        public void EditProduct(Product product)
    => context.Products.Update(product);

        public async Task<FilterProductViewModel> GetFilterProductsAsync(FilterProductViewModel model)
        {
            var query = context.Products.AsQueryable();
            #region Filter
            if (!string.IsNullOrEmpty(model.Title)) 
            {
                query = query.Where(p => p.ProductTitle.Contains(model.Title));
            }
            if (model.Price.HasValue)
            {
                query = query.Where(p => p.Price == model.Price.Value);
            }
            switch (model.Status)
            {
                case FilterProductStatus.NotDeleted:
                    query = query.Where(p => !p.IsDelete);
                    break;
                case FilterProductStatus.All:
                    break;
                case FilterProductStatus.Deleted:
                    query = query.Where(p => p.IsDelete);
                    break;
            }
            #endregion

            query = query.OrderByDescending(p => p.CreateDate);
            await model.Paging(query.Select(p => new ProductViewModel()
            {
                CreateDate = p.CreateDate,
                CategoryId = p.CategoryId,
                Description = p.Description,
                Image = p.Image,
                IsDeleted = p.IsDelete,
                Price = p.Price,
                Title = p.ProductTitle,
                ProductId = p.ProductId,
                Quantity = p.Quantity,
            }));

            return model;
        }

        public async Task<Product?> GetProductByIdAsync(int ProductId)
       => await context.Products.FirstOrDefaultAsync(p => p.ProductId == ProductId);
        public async Task InsertProductAsync(Product product)
      => await context.Products.AddAsync(product);

        public async Task<bool> IsExistSlug(string slug)
      => await context.Products.AnyAsync(p => p.Slug == slug);

        public async Task SaveAsync()
      => await context.SaveChangesAsync();

        public async Task DeleteProductAsync(int poductId)
     => await context.Products.Where(p => p.ProductId == poductId)
            .ExecuteUpdateAsync(s => s.SetProperty(p => p.IsDelete, true));

        public async Task UndoDeleteProductAysnc(int productId)
        => await context.Products.Where(p => p.ProductId == productId)
            .ExecuteUpdateAsync(s => s.SetProperty(p => p.IsDelete, false));

        public async Task<bool> IsExistProductAsync(int productId)
       => await context.Products.AnyAsync(p => p.ProductId == productId);

        public async Task<List<Product>> GetLastesProductsAsync()
     => await context.Products.Include(p => p.ProductColors).Where(p => !p.IsDelete).Take(12).ToListAsync();

        public async Task<FilterClientProductViewModel> GetFilterProductsAsync(FilterClientProductViewModel model)
        {
            var query = context.Products.Include(p => p.ProductCategory).Where((p) => !p.IsDelete).AsQueryable();
            #region Filter
            if (!string.IsNullOrEmpty(model.Tilte))
            {
                query = query.Where(p => p.ProductTitle.Contains(model.Tilte));
            }
            if (model.Price.HasValue)
            {
                query = query.Where(p => p.Price == model.Price.Value);
            }
            if (model.CategoryId.HasValue)
            {
                query = query.Where(p => p.ProductCategory.ParentId == model.CategoryId.Value || p.CategoryId == model.CategoryId);
            }
            #endregion

            query = query.OrderByDescending(p => p.CreateDate);
            await model.Paging(query.Select(p => new LastesProductViewModel()
            {
                Colors = p.ProductColors!.ToList(),
                Price = p.Price,
                ProductId = p.ProductId,
                ProductImage = p.Image,
                ProductTitle = p.ProductTitle,
                Slug = p.Slug,
                Categories = p.ProductCategory.ProductCategories.ToList()
            }));
            return model;
        }

        public async Task<Product> GetProductBySlugAsync(string slug)
      => await context.Products.Include(p => p.ProductFeatures)
            .ThenInclude(p => p.Feature)
            .Include(p => p.ProductColors)
            .Include(p => p.ProductGalleries)
            .Include(p => p.ProductCategory)
            .FirstOrDefaultAsync(p => p.Slug == slug && !p.IsDelete);

        public async Task<List<Product>> GetProductListByCategoryIdAsync(int categoryId)
    =>await context.Products.Include(p=>p.ProductColors).Where(p=>p.CategoryId == categoryId).ToListAsync();

        public async Task InsertInterestsAsync(Interests interests)
   =>await context.Interests.AddAsync(interests);

        public async Task DeleteInterestsAsync(int id)
     =>await context.Interests.Where(i=>i.Id== id).ExecuteDeleteAsync();

        public async Task<Interests?> GetInterestsAsync(int productId, int userId) 
       => await context.Interests.FirstOrDefaultAsync(i => i.UserId == userId && i.ProductId == productId);

        public async Task<List<Interests>> GetInterestsByUserIdAsync(int userId)
       =>await context.Interests.Include(i=>i.Product).Where(i=>i.UserId == userId).ToListAsync();

   
    }
}
