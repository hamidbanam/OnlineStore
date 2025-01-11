using OnlineStore.Application.Extensions;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Application.Static;
using OnlineStore.Domain.InterfaceRepository;
using OnlineStore.Domain.Model.Slider.HomePageSlider;
using OnlineStore.Domain.ViewModel.Slider.HomePageSlider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services.Implentation
{
    public class ClientService(IClientRepository clientRepository) : IClientService
    {
        public async Task<CreateHomePageSliderResult> CreateHomePageSliderAsync(CreateHomePageSliderViewModel model)
        {
            int sliderCount=(await clientRepository.GetAllAsync()).Count();
            if(sliderCount>10)
                return CreateHomePageSliderResult.MoreThanAllowed;
            if (model.SliderAvatar != null)
            {
                HomePageSlider slider= new HomePageSlider();
                slider.SliderImage=NameExtentions.GenerateUnicCod()+Path.GetExtension(model.SliderAvatar.FileName);
                model.SliderAvatar.AddImageToServer(slider.SliderImage, PathTools.Slider);
                slider.SliderUrl=model.SliderUrl;
                slider.SliderTitle=model.SliderTitle;
                slider.IsActive=model.IsActive;
                await clientRepository.InsertHomeSliderAsync(slider);
                await clientRepository.SaveAsync();
            }
            else 
            {
                return CreateHomePageSliderResult.ImageInvalid;
            }
            return CreateHomePageSliderResult.Success;
        }

        public async Task DeleteSliderHomePageAsync(int sliderId)
       =>await clientRepository.DeleteSliderHomePageAsync(sliderId);

        public async Task<EditHomePageSliderViewModel> EditSliderHomePageAsync(int sliderId)
        {
            HomePageSlider slider=await clientRepository.GetHomePageSliderByIdAsync(sliderId);
            EditHomePageSliderViewModel model = new()
            {
                IsActive=slider.IsActive,
                SliderImage=slider.SliderImage,
                SliderTitle=slider.SliderTitle,
                SliderUrl=slider.SliderUrl,
                SliderId=sliderId
            };
            return model;
        }

        public async Task<List<HomePageSliderViewModel>> GetHomePageSliderAsync()
        =>(await clientRepository.GetAllAsync()).Select(s=>new HomePageSliderViewModel()
        {
            IsActive=s.IsActive,
            SliderId=s.SliderId,
            SliderTitle=s.SliderTitle,
            SliderUrl=s.SliderUrl,
            SliderImage=s.SliderImage,
        }).ToList();

        public async Task<EditHomePageSliderResult> UpdateHomePageSliderAsync(EditHomePageSliderViewModel model)
        {
            HomePageSlider slider = await clientRepository.GetHomePageSliderByIdAsync(model.SliderId);
            if (slider == null)
            {
                return EditHomePageSliderResult.NotFound;
            }
            if(model.SliderAvatar != null)
            {
                string imageName=NameExtentions.GenerateUnicCod()+Path.GetExtension(model.SliderAvatar.FileName);
                model.SliderAvatar.AddImageToServer(imageName, PathTools.Slider, null, null, null, slider.SliderImage);
                slider.SliderImage=imageName;
            }
            slider.SliderUrl = model.SliderUrl;
            slider.SliderTitle = model.SliderTitle;
            slider.IsActive = model.IsActive;

            clientRepository.UpdateHomePageSlider(slider);
            await clientRepository.SaveAsync();
            return EditHomePageSliderResult.Success;
        }
    }
}
