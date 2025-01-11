using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.ViewModel.Admin.Feature
{
    public class FeatureViewModel
    {
        public int FeatureId { get; set; }
        public string FeatureTitle { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
