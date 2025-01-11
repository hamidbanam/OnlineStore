using OnlineStore.Application.Extensions;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Application.Static;
using OnlineStore.Domain.InterfaceRepository;
using OnlineStore.Domain.Model.Product.Gallary;
using OnlineStore.Domain.ViewModel.Admin.Gallary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services.Implentation
{
    public class ProductGallaryService(IProductGallaryRepository gallaryRepository) : IProductGallaryService
    {
        public async Task<CreateProductGallaryResult> CreateProductGallaryAsync(CreateProductGallaryViewModel model)
        {
            ProductGallery gallery = new()
            {
                CreateDate = DateTime.Now,
                ImageTitle = model.ImageTitle,
                IsActive = true,
                IsDelete = false,
                ProductId = model.ProductId,
            };
            if (model.ImageName != null)
            {
                gallery.ImageName = NameExtentions.GenerateUnicCod() + Path.GetExtension(model.ImageName.FileName);
                model.ImageName.AddImageToServer(gallery.ImageName, PathTools.ProductImage);
            }
            else
            {
                gallery.ImageName = "NoPhoto.png";
            }
            await gallaryRepository.CreateGallaryAsync(gallery);
            await gallaryRepository.SaveAsync();

            return CreateProductGallaryResult.Success;
        }

        public async Task DeleteProductGallaryAsync(int gallaryId)
    => await gallaryRepository.DeleteGallaryAsync(gallaryId);

        public async Task<EditProductGallartViewModel> EditProductGallaryAsync(int gallaryId)
        {
            ProductGallery gallery = await gallaryRepository.GetGallaryByIdAsync(gallaryId);
            EditProductGallartViewModel model = new()
            {
                GallaryId = gallery.GallaryId,
                ImageTitle = gallery.ImageTitle,
                ProductId = gallery.ProductId
            };
            return model;
        }

        public async Task<List<ProductGallaryViewModel>> GetGallaryByProductIdAsync(int productId)
       => (await gallaryRepository.GetGallaryByProductIdAsync(productId))
            .Select(g => new ProductGallaryViewModel()
            {
                CreateDate = g.CreateDate,
                GallaryId = g.GallaryId,
                ImageName = g.ImageName,
                ImageTitle = g.ImageTitle,
                ProductId = g.ProductId,

            }).ToList();

        public async Task<EditProductGallaryResult> UpdateProductGallary(EditProductGallartViewModel model)
        {
            ProductGallery gallery = await gallaryRepository.GetGallaryByIdAsync(model.GallaryId);
            if (gallery == null)
            {
                return EditProductGallaryResult.NotFound;
            }
            if (model.ImageName != null)
            {
                string imageName = NameExtentions.GenerateUnicCod() + Path.GetExtension(model.ImageName.FileName);
                model.ImageName.AddImageToServer(imageName, PathTools.ProductImage, null, null, null, gallery.ImageName);
                gallery.ImageName = imageName;
            }
            gallery.ImageTitle = model.ImageTitle;
            gallaryRepository.UpdateGallary(gallery);
            await gallaryRepository.SaveAsync();
            return EditProductGallaryResult.Success;

        }
    }
}
