using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.InterfaceRepository;
using OnlineStore.Domain.Model.Product.Feature;
using OnlineStore.Domain.ViewModel.Admin.ProductFeature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services.Implentation
{
    public class ProductFeatureService(
        IProductFeatureRepository productFeature,
        IProductRepository productRepository,
        IFeatureRepository featureRepository,
        IFeatureService featureService
        ) : IProductFeatureService
    {
        public async Task<CreateProductFeatureResult> CreateProductFeatureAsync(CreateProductFeatureViewModel model)
        {
            if (!await HasProductAsync(model.ProductId))
            {
                return CreateProductFeatureResult.ProductNotFound;
            }
            if (!await featureRepository.HasFeatureAsync(model.FeatureId))
            {
                return CreateProductFeatureResult.FeatureNotFound;
            }
            ProductFeature feature = new()
            {
                CreateDate = DateTime.Now,
                FeatureId = model.FeatureId,
                IsActive=true,
                FeatureValue = model.FeatureValue,
                IsDelete=false,
                Order=model.Order,
                ProductId=model.ProductId,
                
            };
            await productFeature.InsertProductFeatureAsync(feature);
            await productFeature.SaveAsync();
            return CreateProductFeatureResult.Success;
        }

        public async Task DeleteProductFeatureAsync(int productFeatureId)
     =>await productFeature.DeleteProductFeatureByIdAsync(productFeatureId);

        public async Task<EditProductFeatureViewModel> EditProductFeatureAsync(int productFeatureId)
        {
            ProductFeature feature=await productFeature.GetProductFeatureByIdAsync(productFeatureId);
            EditProductFeatureViewModel model = new EditProductFeatureViewModel()
            {
                Feature = await featureService.GetAllFeature(),
                ProductId = feature.ProductId,
                FeatureId=feature.FeatureId,
                FeatureValue=feature.FeatureValue,
                Order=feature.Order,
                ProductFeatureId = feature.ProductFeatureId
            };
            return model;
        }

        public async Task<FilterProductFeatureViewModel> FilterProductFeatureAsync(FilterProductFeatureViewModel model)
     =>await productFeature.FilterProductFeatureAsync(model);

        public async Task<bool> HasProductAsync(int productId)
     =>await productRepository.IsExistProductAsync(productId);

        public async Task<EditProductFeatureResult> UpdateProductFeatureAsync(EditProductFeatureViewModel model)
        {
            ProductFeature feature=await productFeature.GetProductFeatureByIdAsync(model.ProductFeatureId);
            if (feature == null)
            {
                return EditProductFeatureResult.NotFound;
            }
            feature.FeatureValue = model.FeatureValue;
            feature.Order = model.Order;
            feature.ProductId = model.ProductId;
            feature.FeatureId = model.FeatureId;
            
            productFeature.UpdateProductFeature(feature);
            await productFeature.SaveAsync();
            return EditProductFeatureResult.Success;
        }
    }
}
