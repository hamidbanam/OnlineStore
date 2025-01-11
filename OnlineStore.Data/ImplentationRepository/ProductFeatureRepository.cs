using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Context;
using OnlineStore.Domain.InterfaceRepository;
using OnlineStore.Domain.Model.Product.Feature;
using OnlineStore.Domain.ViewModel.Admin.ProductFeature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Data.ImplentationRepository
{
    public class ProductFeatureRepository(OnlineStoreContext context) : IProductFeatureRepository
    {
        public async Task DeleteProductFeatureByIdAsync(int productFeatureId)
      => await context.ProductFeatures.Where(pf => pf.ProductFeatureId == productFeatureId).ExecuteDeleteAsync();

        public async Task<FilterProductFeatureViewModel> FilterProductFeatureAsync(FilterProductFeatureViewModel model)
        {
            var query = context.ProductFeatures
                .Include(pf=>pf.Product)
                .Include(pf=>pf.Feature) 
                .AsQueryable();
            #region Filter
            if (!string.IsNullOrEmpty(model.Title))
            {
                query = query.Where(pf => pf.FeatureValue.Contains(model.Title));
            }
            #endregion
            if (model.ProductId.HasValue)
                query = query.Where(pf => pf.ProductId == model.ProductId.Value);

            query = query.OrderByDescending(pf => pf.Order);
            await model.Paging(query.Select(pf => new ProductFeatureViewModel()
            {
                Order = pf.Order,
                FeatureId = pf.FeatureId,
                FeatureValue = pf.FeatureValue,
                ProductId = pf.ProductId,
                ProductFeatureId = pf.ProductFeatureId,
                ProductTitle=pf.Product!.ProductTitle,
                FeatureTitle=pf.Feature!.Title,
            }));
            return model;
        }

        public async Task<ProductFeature?> GetProductFeatureByIdAsync(int productFeatureId)
       =>await context.ProductFeatures.FirstOrDefaultAsync(pf=>pf.ProductFeatureId==productFeatureId);

        public async Task InsertProductFeatureAsync(ProductFeature productFeature)
      =>await context.ProductFeatures.AddAsync(productFeature);

        public Task SaveAsync()
       => context.SaveChangesAsync();

        public void UpdateProductFeature(ProductFeature productFeature)
      =>context.ProductFeatures.Update(productFeature);
    }
}
