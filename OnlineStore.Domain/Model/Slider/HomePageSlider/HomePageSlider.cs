using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Domain.Model.Slider.HomePageSlider
{
    public class HomePageSlider
    {
        [Key] 
        public int SliderId { get; set; }

        [MaxLength(200)]
        public string SliderTitle { get; set; }
        public string SliderImage { get; set; }
        public string SliderUrl { get; set; }
        public bool IsActive { get; set; }
    }
}
