using System;
using System.Collections.Generic;
using FinanceOne.Domain.Entities;
using FinanceOne.Domain.ViewModels.CategoryViewModels;
using FinanceOne.Shared.Exceptions;
using FinanceOne.Shared.Repositories;

namespace FinanceOne.Domain.Services
{
  public interface ICategoryService : ICategoryServiceStaticMembers
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
