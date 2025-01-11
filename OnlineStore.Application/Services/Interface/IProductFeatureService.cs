using OnlineStore.Domain.ViewModel.Admin.ProductFeature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services.Interface
{
    public interface IProductFeatureService
    {
        Task<FilterProductFeatureViewModel> FilterProductFeatureAsync(FilterProductFeatureViewModel model);
        Task<CreateProductFeatureResult> CreateProductFeatureAsync(CreateProductFeatureViewModel model);
        Task<bool> HasProductAsync(int  productId);
        Task<EditProductFeatureViewModel> EditProductFeatureAsync(int productFeatureId);
        Task DeleteProductFeatureAsync(int productFeatureId);
        Task<EditProductFeatureResult> UpdateProductFeatureAsync(EditProductFeatureViewModel model);
    }
}
