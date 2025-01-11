using OnlineStore.Domain.Model.Feature;
using OnlineStore.Domain.ViewModel.Admin.Feature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.InterfaceRepository
{
    public interface IFeatureRepository
    {
        Task<FilterFeatureViewModel> GetFilterFeatureAsync(FilterFeatureViewModel model);
        Task InsertFeatureAsync(Feature feature);
        Task SaveAsync();
        Task<Feature?> GetFeatureByIdAsync(int featureId);
        Task UpdateFeatureAsync(EditFeatureViewModel model);
        Task DeleteFeatureAsync(int featureId);
        Task<List<Feature>?> GetAllFeature();
        Task<bool> HasFeatureAsync(int featureId);
    }
}
