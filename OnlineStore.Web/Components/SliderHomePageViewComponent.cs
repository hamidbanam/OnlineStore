using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.ViewModel.Slider.HomePageSlider;

namespace OnlineStore.Web.Components
{
    public class SliderHomePageViewComponent(IClientService clientService):ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<HomePageSliderViewModel> model=await clientService.GetHomePageSliderAsync();
            return View("SliderHomePage",model);
        }
    }
}
