using OnlineStore.Domain.ViewModel.Admin.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services.Interface
{
    public interface ICategoryService
    {
        Task<FilterCategoryViewModel> GetFilterCategoryAsync(FilterCategoryViewModel model);
        Task<CreateCategoryResult> CreateCategoryAsync(CreateCategoryViewModel model);
        Task<List<CategoryListViewModel>> GetAllCategoryAsync();
        Task<EditCategoryResult> EditCategoryAsync(EditCategoryViewModel model);
        Task<DeleteCategoryResult> DeleteCategoryAsync(int categoryId);
        Task<EditCategoryViewModel> GetEditCategoryAsync(int categoryId);
        Task<string> GetCategoryById(int categoryId);


    }
}
