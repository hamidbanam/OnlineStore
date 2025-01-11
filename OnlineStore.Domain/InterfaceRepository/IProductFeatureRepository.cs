using OnlineStore.Domain.Model.Product.Feature;
using OnlineStore.Domain.ViewModel.Admin.ProductFeature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.InterfaceRepository
{
    public interface IProductFeatureRepository
    {
        Task<FilterProductFeatureViewModel> FilterProductFeatureAsync(FilterProductFeatureViewModel model);
        Task InsertProductFeatureAsync(ProductFeature productFeature);
        Task SaveAsync();
        Task<ProductFeature> GetProductFeatureByIdAsync(int productFeatureId);
        void UpdateProductFeature(ProductFeature productFeature);
        Task DeleteProductFeatureByIdAsync(int productFeatureId);
    }
}
