using System.Collections.Generic;
using FinanceOne.Domain.ViewModels.CategoryViewModels;
using FinanceOne.Domain.ViewModels.UserViewModels;

namespace FinanceOne.Domain.Services
{
  public interface ICategoryService
  {
    ShowCategoryResponseViewModel CreateCategory(
      CreateCategoryViewModel createCategoryViewModel
    );

    ShowCategoryResponseViewModel UpdateCategory(
      UpdateCategoryViewModel updateCategoryViewModel
    );

    IList<ShowCategoryResponseViewModel> ListCategories(
      ListCategoriesViewModel listCategoriesViewModel
    );

    ShowCategoryResponseViewModel GetCategory(
      GetCategoryViewModel getCategoryViewModel
    );

    void DeleteCategory(DeleteCategoryViewModel deleteCategoryViewModel);
  }
}
