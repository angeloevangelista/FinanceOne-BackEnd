using System;
using System.Collections.Generic;
using System.Linq;
using FinanceOne.Domain.Entities;
using FinanceOne.Domain.Services;
using FinanceOne.Domain.ViewModels.CategoryViewModels;
using FinanceOne.Shared.Exceptions;
using FinanceOne.Shared.Repositories;

namespace FinanceOne.Implementation.Services
{
  public class CategoryService : ICategoryService
  {
    private readonly IUserRepository _userRepository;
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(
      IUserRepository userRepository,
      ICategoryRepository categoryRepository
    )
    {
      this._categoryRepository = categoryRepository;
      this._userRepository = userRepository;
    }

    public ShowCategoryResponseViewModel CreateCategory(
      CreateCategoryViewModel createCategoryViewModel
    )
    {
      var foundUser = this._userRepository.FindById(new User()
      {
        Id = Guid.Parse(createCategoryViewModel.UserId)
      });

      if (foundUser == null)
        throw new BusinessException("User not found.");

      var category = new Category()
      {
        Id = Guid.NewGuid(),
        Name = createCategoryViewModel.Name,
        Description = createCategoryViewModel.Description,
        UserId = foundUser.Id
      };

      var existingCategories = this._categoryRepository.ListByUser(category);

      var categoryAlreadyExists = existingCategories
        .Any(p => p.Name.ToLower() == category.Name.ToLower());

      if (categoryAlreadyExists)
        throw new BusinessException("Category already exists.");

      category = this._categoryRepository.Create(category);

      return ShowCategoryResponseViewModel.ConvertFromEntity(category);
    }

    public void DeleteCategory(DeleteCategoryViewModel deleteCategoryViewModel)
    {
      var foundCategory = ICategoryService.ValidateCategoryWasCreatedByUser(
        this._categoryRepository,
        this._userRepository,
        deleteCategoryViewModel.Id,
        deleteCategoryViewModel.UserId
      );

      foundCategory.UpdatedAt = DateTime.UtcNow;

      this._categoryRepository.Delete(foundCategory);
    }

    public ShowCategoryResponseViewModel GetCategory(
      GetCategoryViewModel getCategoryViewModel
    )
    {
      var foundCategory = ICategoryService.ValidateCategoryWasCreatedByUser(
        this._categoryRepository,
        this._userRepository,
        getCategoryViewModel.Id,
        getCategoryViewModel.UserId
      );

      if (foundCategory == null)
        throw new BusinessException("Category not found.");

      return ShowCategoryResponseViewModel.ConvertFromEntity(foundCategory);
    }

    public IList<ShowCategoryResponseViewModel> ListCategories(
      ListCategoriesViewModel listCategoriesViewModel
    )
    {
      var foundUser = this._userRepository.FindById(new User()
      {
        Id = Guid.Parse(listCategoriesViewModel.UserId)
      });

      if (foundUser == null)
        throw new BusinessException("User not found.");

      var categories = this._categoryRepository.ListByUser(new Category()
      {
        UserId = foundUser.Id
      });

      return categories
        .Select(p => ShowCategoryResponseViewModel.ConvertFromEntity(p))
        .ToList();
    }

    public ShowCategoryResponseViewModel UpdateCategory(
      UpdateCategoryViewModel updateCategoryViewModel
    )
    {
      var foundCategory = ICategoryService.ValidateCategoryWasCreatedByUser(
        this._categoryRepository,
        this._userRepository,
        updateCategoryViewModel.Id,
        updateCategoryViewModel.UserId
      );

      var updateName =
        updateCategoryViewModel.Name != foundCategory.Name;

      if (updateName)
      {
        var existingCategories = this._categoryRepository.ListByUser(
          foundCategory
        );

        var categoryAlreadyExists = existingCategories.Any(p =>
          p.Id != foundCategory.Id
          && p.Name.ToLower() == updateCategoryViewModel.Name.ToLower()
        );

        if (categoryAlreadyExists)
          throw new BusinessException("Duplicated categories are not allowed.");
        else
          foundCategory.Name = updateCategoryViewModel.Name;
      }

      foundCategory.Description = updateCategoryViewModel.Description;
      foundCategory.UpdatedAt = DateTime.Now;

      foundCategory = this._categoryRepository.Update(foundCategory);

      return ShowCategoryResponseViewModel.ConvertFromEntity(foundCategory);
    }
  }
}
