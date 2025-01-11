using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.InterfaceRepository;
using OnlineStore.Domain.Model.Feature;
using OnlineStore.Domain.ViewModel.Admin.Feature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services.Implentation
{
    public class FeatureService(IFeatureRepository featureRepository) : IFeatureService
    {
        public async Task<CreateFeatureResult> CreateFeatureAsync(CreateFeatureViewModel model)
        {
            Feature feature = new()
            {
                Title = model.Title,
                CreateDate = DateTime.Now,
                IsActive = true,
                IsDelete = false,

            };
            await featureRepository.InsertFeatureAsync(feature);
            await featureRepository.SaveAsync();
            return CreateFeatureResult.Success;

        }

        public async Task<DeleteFeatureResult> DeleteFeatureAsync(int featureId)
        {
            if (featureId == null || featureId == 0)
            {
                return DeleteFeatureResult.NotFound;
            }
            await featureRepository.DeleteFeatureAsync(featureId);
            await featureRepository.SaveAsync();
            return DeleteFeatureResult.Success;
        }

        public async Task<EditFeatureResult> EditFeatureAsync(EditFeatureViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Title))
            {
                return EditFeatureResult.NotFound;
            }
            await featureRepository.UpdateFeatureAsync(model);
            await featureRepository.SaveAsync();
            return EditFeatureResult.Success;
        }

        public async Task<EditFeatureViewModel> GetEditFeatureAsync(int featureId)
        {
            Feature feature = await featureRepository.GetFeatureByIdAsync(featureId);
            EditFeatureViewModel model = new EditFeatureViewModel()
            {
                FeatureId = feature.FeatureId,
                Title = feature.Title,
            };
            return model;
        }

        public async Task<List<FeatureViewModel>> GetAllFeature()
=> (await featureRepository.GetAllFeature()).Select(f => new FeatureViewModel()
{
    FeatureId = f.FeatureId,
    CreateDate = f.CreateDate,
    FeatureTitle = f.Title,
    IsActive = f.IsActive,
    IsDelete = f.IsDelete,
}).ToList();

        public async Task<FilterFeatureViewModel> GetFilterFeatureAsync(FilterFeatureViewModel model)
      => await featureRepository.GetFilterFeatureAsync(model);
    }
}
