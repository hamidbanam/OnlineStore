using OnlineStore.Application.Extensions;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Application.Static;
using OnlineStore.Domain.InterfaceRepository;
using OnlineStore.Domain.Model.Product.Category;
using OnlineStore.Domain.ViewModel.Admin.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services.Implentation
{
    public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
    {
        public async Task<CreateCategoryResult> CreateCategoryAsync(CreateCategoryViewModel model)
        {

            ProductCategory category = new ProductCategory()
            {
                CreateDate = DateTime.Now,
                IsActive = true,
                ParentId = model.ParentId,
                Title = model.CategoryTitle,
                IsDelete = false,

            };
            if (model.CategoryAvatar != null)
            {
                category.ImageCategory = NameExtentions.GenerateUnicCod() + Path.GetExtension(model.CategoryAvatar.FileName);
                model.CategoryAvatar.AddImageToServer(category.ImageCategory, PathTools.CategoryImage);
            }
            else
            {
                category.ImageCategory = "NoPhoto.png";
            }
            await categoryRepository.InsertCategoryAsync(category);
            await categoryRepository.SaveAsync();
            return CreateCategoryResult.Success;
        }


        public async Task<FilterCategoryViewModel> GetFilterCategoryAsync(FilterCategoryViewModel model)
      => await categoryRepository.GetFilterCategoryAsync(model);

        public async Task<List<CategoryListViewModel>> GetAllCategoryAsync()
        {
            return categoryRepository.GetAllCategoryAsync().Result
                .Select(c => new CategoryListViewModel()
                {
                    CategoryId = c.CatrgoryId,
                    CategoryTitle = c.Title,
                    CreateDate = c.CreateDate,
                    ParentId = c.ParentId,
                    ImageCategory = c.ImageCategory,
                }).ToList();
        }

        public async Task<EditCategoryResult> EditCategoryAsync(EditCategoryViewModel model)
        {
            if (model.CategoryTitle == null)
            {
                return EditCategoryResult.NotFound;
            }
            await categoryRepository.UpdateCategoryAsync(model);
            if (model.CategoryAvatar != null)
            {
                ProductCategory category = await categoryRepository.GetCategoryByIdAsync(model.CategoryId);
                string imageName = NameExtentions.GenerateUnicCod() + Path.GetExtension(model.CategoryAvatar.FileName);
                model.CategoryAvatar.AddImageToServer(imageName, PathTools.CategoryImage, null, null, null, category.ImageCategory);
                category.ImageCategory = imageName;
                categoryRepository.UpdateCategory(category);
                await categoryRepository.SaveAsync();
            }
            return EditCategoryResult.Success;
        }

        public async Task<DeleteCategoryResult> DeleteCategoryAsync(int categoryId)
        {
            if (categoryId == null || categoryId <= 0)
            {
                return DeleteCategoryResult.NotFound;
            }
            await categoryRepository.DeleteCategoryAsync(categoryId);
            return DeleteCategoryResult.Success;
        }

        public async Task<EditCategoryViewModel> GetEditCategoryAsync(int categoryId)
        {
            ProductCategory category = await categoryRepository.GetCategoryByIdAsync(categoryId);
            EditCategoryViewModel model = new EditCategoryViewModel()
            {
                Categories = await GetAllCategoryAsync(),
                CategoryTitle = category.Title,
                ParentId = category.ParentId,
            };
            return model;
        }

        public async Task<string> GetCategoryById(int categoryId)
        {
            ProductCategory category=await categoryRepository.GetCategoryByIdAsync(categoryId);
            CategoryListViewModel model = new()
            {
                CreateDate = category.CreateDate,
                CategoryId = category.CatrgoryId,
                CategoryTitle = category.Title,
                ImageCategory = category.ImageCategory,
                ParentId= category.ParentId,
              
            };
            return model.CategoryTitle;
        }
    }
}
