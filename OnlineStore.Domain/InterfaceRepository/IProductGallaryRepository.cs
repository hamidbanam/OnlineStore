using OnlineStore.Domain.Model.Product.Gallary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.InterfaceRepository
{
    public interface IProductGallaryRepository
    {
        Task<List<ProductGallery>> GetGallaryByProductIdAsync(int productId);
        Task CreateGallaryAsync(ProductGallery gallery);
        Task SaveAsync();
        void UpdateGallary(ProductGallery gallery);
        Task DeleteGallaryAsync(int gallaryId);
        Task<ProductGallery> GetGallaryByIdAsync(int gallaryId);
    }
}
