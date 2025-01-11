using OnlineStore.Domain.Model.Feature;
using OnlineStore.Domain.ViewModel.Admin.Feature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services.Interface
{
    public interface IFeatureService
    {
        Task<FilterFeatureViewModel> GetFilterFeatureAsync(FilterFeatureViewModel model);
        Task<CreateFeatureResult> CreateFeatureAsync(CreateFeatureViewModel model);
        Task<EditFeatureResult> EditFeatureAsync(EditFeatureViewModel model);
        Task<DeleteFeatureResult> DeleteFeatureAsync(int featureId);
        Task<EditFeatureViewModel> GetEditFeatureAsync(int featureId);
        Task<List<FeatureViewModel>> GetAllFeature();
    }
}
