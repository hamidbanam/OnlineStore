using OnlineStore.Application.Extensions;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.InterfaceRepository;
using OnlineStore.Domain.Model.Product.Color;
using OnlineStore.Domain.ViewModel.Admin.Color;
using OnlineStore.Domain.ViewModel.Admin.Product;
using OnlineStore.Domain.ViewModel.Client.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services.Implentation
{
    public class ProductColorService(IProductColorRepository colorRepository) : IProductColorService
    {
        public async Task<CreateProductColorResult> CreateProductColorAsync(CreateProductColorViewModel model)
        {
            ProductColor productColor = new ProductColor()
            {
                Color = model.Color,
                ColorTitle = model.ColorTitle,
                CreateDate = DateTime.Now,
                IsActive = true,
                IsDelete = false,
                Price = model.Price,
                ProductId = model.ProductId,
                Quantity = model.Quantity,

            };
            await colorRepository.InsertProductColorAsync(productColor);
            await colorRepository.SaveAsync();
            return CreateProductColorResult.Success;
        }

        public async Task DeleteProductColorAsync(int colorId)
     => await colorRepository.DeleteProductColorAsync(colorId);

        public async Task<ChangeColorViewModel> GetColorByIdAsync(int id)
        {
            ProductColor color = await colorRepository.GetProductColorByIdAsync(id);
            return new()
            {
                Id = color.ColorId,
                ColorName = color.Color,
                Price = color.Price.ToMoney(),
                ColorTitle = color.ColorTitle,
            };
        }

        public async Task<List<ProductColorViewModel>> GetProductColorByProductIdAsync(int productId)
     => (await colorRepository.GetProductColorByProductIdAsync(productId)).Select(c => new ProductColorViewModel()
     {
         ProductId = c.ProductId,
         Color = c.Color,
         ColorId = c.ColorId,
         ColorTitle = c.ColorTitle,
         Price = c.Price,
         ProductTitle = c.Product!.ProductTitle,
         Quantity = c.Quantity,
     }).ToList();
    }
}
