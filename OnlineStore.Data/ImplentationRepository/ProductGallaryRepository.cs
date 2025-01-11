using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Context;
using OnlineStore.Domain.InterfaceRepository;
using OnlineStore.Domain.Model.Product.Gallary;


namespace OnlineStore.Data.ImplentationRepository
{
    public class ProductGallaryRepository(OnlineStoreContext context) : IProductGallaryRepository
    {
        public async Task CreateGallaryAsync(ProductGallery gallery)
   => await context.ProductGalleries.AddAsync(gallery);

        public async Task DeleteGallaryAsync(int gallaryId)
     =>await context.ProductGalleries.Where(g=>g.GallaryId==gallaryId).ExecuteDeleteAsync();

        public async Task<ProductGallery?> GetGallaryByIdAsync(int gallaryId)
       => await context.ProductGalleries.FirstOrDefaultAsync(g => g.GallaryId == gallaryId);

        public async Task<List<ProductGallery>> GetGallaryByProductIdAsync(int productId)
       =>await context.ProductGalleries.Where(g=>g.ProductId==productId).ToListAsync();

        public async Task SaveAsync()
        =>await context.SaveChangesAsync();

        public void UpdateGallary(ProductGallery gallery)
      => context.ProductGalleries.Update(gallery);
    }
}
