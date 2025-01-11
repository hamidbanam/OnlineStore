using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Context;
using OnlineStore.Domain.InterfaceRepository;
using OnlineStore.Domain.Model.Product.Color;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Data.ImplentationRepository
{
    public class ProductColorRepository(OnlineStoreContext context) : IProductColorRepository
    {
        public async Task DeleteProductColorAsync(int colorId)
     => await context.productColors.Where(c => c.ColorId == colorId).ExecuteDeleteAsync();

        public async Task<ProductColor?> GetProductColorByIdAsync(int colorId)
     => await context.productColors.FirstOrDefaultAsync(c => c.ColorId == colorId);

        public async Task<List<ProductColor>> GetProductColorByProductIdAsync(int productId)
=> await context.productColors.Include(c => c.Product).Where(c => c.ProductId == productId).ToListAsync();

        public async Task InsertProductColorAsync(ProductColor productColor)
    => await context.productColors.AddAsync(productColor);

        public async Task SaveAsync()
      => await context.SaveChangesAsync();

        public void UpdateProductColor(ProductColor productColor)
      =>context.productColors.Update(productColor);
    }
}
