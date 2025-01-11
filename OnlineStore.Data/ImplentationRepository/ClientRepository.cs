using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Context;
using OnlineStore.Domain.InterfaceRepository;
using OnlineStore.Domain.Model.Slider.HomePageSlider;
using OnlineStore.Domain.ViewModel.Slider.HomePageSlider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Data.ImplentationRepository
{
    public class ClientRepository(OnlineStoreContext context) : IClientRepository
    {
        public async Task DeleteSliderHomePageAsync(int sliderId)
     =>await context.Sliders.Where(s=>s.SliderId==sliderId).ExecuteDeleteAsync();

        public async Task<List<HomePageSlider>> GetAllAsync()
 => await context.Sliders.ToListAsync();

        public async Task<HomePageSlider?> GetHomePageSliderByIdAsync(int sliderId)
   => await context.Sliders.FirstOrDefaultAsync(s => s.SliderId == sliderId);

        public async Task InsertHomeSliderAsync(HomePageSlider slider)
       => await context.AddAsync(slider);

        public async Task SaveAsync()
      => await context.SaveChangesAsync();

        public void UpdateHomePageSlider(HomePageSlider slider)
       =>context.Sliders.Update(slider);
    }
}
