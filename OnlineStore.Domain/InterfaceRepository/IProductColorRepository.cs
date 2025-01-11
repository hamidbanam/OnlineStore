using OnlineStore.Domain.Model.Product.Color;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.InterfaceRepository
{
    public interface IProductColorRepository
    {
        Task<List<ProductColor>> GetProductColorByProductIdAsync(int productId);
        Task<ProductColor> GetProductColorByIdAsync(int colorId);
        Task InsertProductColorAsync(ProductColor productColor);
        Task DeleteProductColorAsync(int colorId);
        Task SaveAsync();
        void UpdateProductColor(ProductColor productColor);
    }
}
