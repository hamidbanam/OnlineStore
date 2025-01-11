using OnlineStore.Domain.Model.Slider.HomePageSlider;
using OnlineStore.Domain.ViewModel.Slider.HomePageSlider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.InterfaceRepository
{
    public interface IClientRepository
    {
        Task InsertHomeSliderAsync(HomePageSlider slider);
        Task SaveAsync();
        Task<List<HomePageSlider>> GetAllAsync();
       void UpdateHomePageSlider(HomePageSlider slider);
        Task<HomePageSlider> GetHomePageSliderByIdAsync(int sliderId);
        Task DeleteSliderHomePageAsync(int sliderId);
     
    }
}
