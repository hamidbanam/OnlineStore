using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Context;
using OnlineStore.Domain.InterfaceRepository;
using OnlineStore.Domain.Model.Feature;
using OnlineStore.Domain.ViewModel.Admin.Feature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Data.ImplentationRepository
{
    public class FeatureRepository(OnlineStoreContext context) : IFeatureRepository
    {
        public async Task DeleteFeatureAsync(int featureId)
      =>await context.Features.Where(f=> f.FeatureId == featureId)
            .ExecuteUpdateAsync(s=>s.SetProperty(s=>s.IsDelete,true));

        public async Task<List<Feature>?> GetAllFeature()
     =>await context.Features.ToListAsync();

        public async Task<Feature?> GetFeatureByIdAsync(int featureId)
      => await context.Features.FirstOrDefaultAsync(f => f.FeatureId == featureId);

        public async Task<FilterFeatureViewModel> GetFilterFeatureAsync(FilterFeatureViewModel model)
        {
            var query= context.Features.Where(f=>!f.IsDelete).AsQueryable();

            #region Filter
            if(!string.IsNullOrEmpty(model.Title))
            {
                query=query.Where(f=>f.Title.Contains(model.Title));
            }
            #endregion

            query=query.OrderByDescending(f=>f.CreateDate);

            await model.Paging(query.Select(f => new FeatureViewModel()
            {
                CreateDate = DateTime.Now,
                FeatureTitle =f.Title,
                FeatureId=f.FeatureId,
                IsActive=true,
                
            }));
            return model;
        }

        public async Task<bool> HasFeatureAsync(int featureId)
      =>await context.Features.AnyAsync(f => f.FeatureId == featureId);

        public async Task InsertFeatureAsync(Feature feature)
     =>await context.Features.AddAsync(feature);

        public async Task SaveAsync()
      =>await context.SaveChangesAsync();

        public async Task UpdateFeatureAsync(EditFeatureViewModel model)
        => await context.Features.Where(f => f.FeatureId == model.FeatureId)
            .ExecuteUpdateAsync(s => s.SetProperty(
                s => s.Title, model.Title));
    }
}
