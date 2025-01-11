using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Slider.HomePageSlider
{
    public class HomePageSliderViewModel
    {
        public int SliderId { get; set; }
        public string SliderTitle { get; set; }
        public string SliderImage { get; set; }
        public string SliderUrl { get; set; }
        public bool IsActive { get; set; }
    }
}
