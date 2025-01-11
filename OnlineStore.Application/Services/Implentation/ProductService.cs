using OnlineStore.Application.Extensions;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Application.Static;
using OnlineStore.Domain.Enum.Product;
using OnlineStore.Domain.InterfaceRepository;
using OnlineStore.Domain.Model.Interests;
using OnlineStore.Domain.Model.Product.Comment;
using OnlineStore.Domain.Model.Product.Product;
using OnlineStore.Domain.ViewModel.Admin.Product;
using OnlineStore.Domain.ViewModel.Client.Interests;
using OnlineStore.Domain.ViewModel.Client.Product;
using OnlineStore.Domain.ViewModel.Client.ProductComment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services.Implentation
{
    public class ProductService(IProductRepository productRepository,
            ICategoryService categoryService) : IProductService
    {
        public async Task<CreateProductResult> CreateProductAsync(CreateProductViewModel model)
        {
            if (await productRepository.IsExistSlug(model.Slug))
            {
                return CreateProductResult.SlugExits;
            }
            Product product = new()
            {
                Slug = model.Slug,
                CategoryId = model.CategoryId,
                CreateDate = DateTime.Now,
                Description = model.Description,
                IsActive = model.IsActive,
                IsDelete = false,
                IsFreeShipping = model.IsFreeShipping,
                Price = model.Price,
                ProductTitle = model.ProductTitle,
                Quantity = model.Quantity,
                Review = model.Review,
                TitleForEnglish = model.TitleForEnglish,
                WarningDescription = model.WarningDescription,
                WarrantyText = model.WarrantyText,

            };
            if (model.Image != null)
            {
                product.Image = NameExtentions.GenerateUnicCod() + Path.GetExtension(model.Image.FileName);
                model.Image.AddImageToServer(product.Image, PathTools.ProductImage);
            }
            else
            {
                product.Image = "NoPhoto.png";
            }
            await productRepository.InsertProductAsync(product);
            await productRepository.SaveAsync();

            return CreateProductResult.Success;
        }

        public async Task DeleteInterestsAsync(int id)
       =>await productRepository.DeleteInterestsAsync(id);

        public async Task<DeleteProductResult> DeleteProductAsync(int productId)
        {
            Product product = await productRepository.GetProductByIdAsync(productId);
            if (product == null)
            {
                return DeleteProductResult.NotFound;
            }
            if (!product.IsDelete)
            {
                await productRepository.DeleteProductAsync(productId);
            }
            else
            {
                await productRepository.UndoDeleteProductAysnc(productId);
            }

            return DeleteProductResult.Success;
        }

        public async Task<EditProductViewModel> GetEditProductAsync(int productId)
        {
            Product product = await productRepository.GetProductByIdAsync(productId);
            EditProductViewModel model = new EditProductViewModel
            {
                ProductId = product.ProductId,
                Categories = await categoryService.GetAllCategoryAsync(),
                CategoryId = product.CategoryId,
                Description = product.Description,
                ImageName = product.Image,
                IsActive = product.IsActive,
                IsFreeShipping = product.IsFreeShipping,
                Price = product.Price,
                ProductTitle = product.ProductTitle,
                Quantity = product.Quantity,
                Review = product.Review,
                Slug = product.Slug,
                TitleForEnglish = product.TitleForEnglish,
                WarningDescription = product.WarningDescription,
                WarrantyText = product.WarrantyText,
            };
            return model;
        }

        public async Task<FilterProductViewModel> GetFilterProductsAsync(FilterProductViewModel model)
       => await productRepository.GetFilterProductsAsync(model);

        public async Task<FilterClientProductViewModel> GetFilterProductsAsync(FilterClientProductViewModel model)
     => await productRepository.GetFilterProductsAsync(model);

        public async Task<Interests> GetInterestsAsync(int productId, int userId)
      =>await productRepository.GetInterestsAsync(productId, userId);

        public async Task<List<InterestsViewModel>> GetInterestsByUserId(int userId)
      =>(await productRepository.GetInterestsByUserIdAsync(userId)).Select( i=>new InterestsViewModel()
      {
          Id = i.Id,
          UserId = i.UserId,
          Product=i.Product
      }).ToList();

        public async Task<List<LastesProductViewModel>> GetLastesProductsAsync()
        {
            return (await productRepository.GetLastesProductsAsync()).Select(p => new LastesProductViewModel()
            {
                ProductTitle = p.ProductTitle,
                Price = p.Price,
                ProductId = p.ProductId,
                ProductImage = p.Image,
                Colors = p.ProductColors!.ToList(),
                Slug = p.Slug

            }).ToList();
        }

        public async Task<ProductDetailViewModel> GetProductDetailAsync(string slug)
        {
            Product product = await productRepository.GetProductBySlugAsync(slug);
            if (product == null)
            {
                return null;
            }

            return new ProductDetailViewModel
            {
                ProductId = product.ProductId,
                ProductTitle = product.ProductTitle,
                CategoryId = product.CategoryId,
                Category = product.ProductCategory,
                Colors = product.ProductColors,
                Description = product.Description,
                Features = product.ProductFeatures,
                Galleries = product.ProductGalleries,
                Image = product.Image,
                IsFreeShipping = product.IsFreeShipping,
                Price = product.Price,
                Quantity = product.Quantity,
                Review = product.Review,
                Slug = product.Slug,
                TitleForEnglish = product.TitleForEnglish,
                WarningDescription = product.WarningDescription,
                WarrantyText = product.WarrantyText,
            };
        }

        public async Task<List<LastesProductViewModel>> GetProductsByCategoryIdAsync(int categoryId)
  => (await productRepository.GetProductListByCategoryIdAsync(categoryId)).Select(p => new LastesProductViewModel
  {
      Colors = p.ProductColors.ToList(),
      Price = p.Price,
      ProductId = p.ProductId,
      ProductImage = p.Image,
      ProductTitle = p.ProductTitle,
      Slug = p.Slug,
  }).ToList();

        public async Task InterestsAsync(int productId, int userId)
        {
            Interests interests=await productRepository.GetInterestsAsync(productId, userId);
            if (interests != null)
            {
                await productRepository.DeleteInterestsAsync(interests.Id);
            }
            else
            {
                interests = new()
                {
                    ProductId = productId,
                    UserId = userId,
                    CreateDate = DateTime.Now,
                };
                await productRepository.InsertInterestsAsync(interests);
                await productRepository.SaveAsync();
            }
        }

        public async Task<EditProductResult> UpdateProductAsync(EditProductViewModel model)
        {
            Product product = await productRepository.GetProductByIdAsync(model.ProductId);
            if (product == null)
            {
                return EditProductResult.NotFound;
            }
            if (product.Slug != model.Slug)
            {
                if (await productRepository.IsExistSlug(model.Slug))
                {
                    return EditProductResult.SlugExits;
                }
            }
            if (model.NewImage != null)
            {
                string imageName = NameExtentions.GenerateUnicCod() + Path.GetExtension(model.NewImage.FileName);
                model.NewImage.AddImageToServer(imageName, PathTools.ProductImage, null, null, null, product.Image);
                product.Image = imageName;
            }
            product.Slug = model.Slug;
            product.WarrantyText = model.WarrantyText;
            product.ProductTitle = model.ProductTitle;
            product.Quantity = model.Quantity;
            product.Review = model.Review;
            product.Description = model.Description;
            product.CategoryId = model.CategoryId;
            product.IsActive = model.IsActive;
            product.TitleForEnglish = model.TitleForEnglish;
            product.IsFreeShipping = model.IsFreeShipping;
            product.WarningDescription = model.WarningDescription;
            product.Price = model.Price;

            productRepository.EditProduct(product);
            await productRepository.SaveAsync();
            return EditProductResult.Success;
        }
    }
}
