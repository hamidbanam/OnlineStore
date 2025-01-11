using OnlineStore.Domain.ViewModel.Slider.HomePageSlider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services.Interface
{
    public interface IClientService
    {
        Task<CreateHomePageSliderResult> CreateHomePageSliderAsync(CreateHomePageSliderViewModel model);
        Task<List<HomePageSliderViewModel>> GetHomePageSliderAsync(); 
        Task<EditHomePageSliderViewModel> EditSliderHomePageAsync(int sliderId);
        Task<EditHomePageSliderResult> UpdateHomePageSliderAsync(EditHomePageSliderViewModel model);
        Task DeleteSliderHomePageAsync(int sliderId);
    }
}
