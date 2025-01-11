using OnlineStore.Domain.ViewModel.Admin.Gallary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services.Interface
{
    public interface IProductGallaryService
    {
        Task<List<ProductGallaryViewModel>> GetGallaryByProductIdAsync(int productId);
        Task<CreateProductGallaryResult> CreateProductGallaryAsync(CreateProductGallaryViewModel model);
        Task DeleteProductGallaryAsync(int gallaryId);
        Task<EditProductGallartViewModel> EditProductGallaryAsync(int gallaryId);
        Task<EditProductGallaryResult> UpdateProductGallary(EditProductGallartViewModel model);
    }
}
