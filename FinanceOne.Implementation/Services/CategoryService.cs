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

      return ShowCategoryResponseViewModel.ConvertFromCategory(category);
    }

    public void DeleteCategory(DeleteCategoryViewModel deleteCategoryViewModel)
    {
      var foundUser = this._userRepository.FindById(new User()
      {
        Id = Guid.Parse(deleteCategoryViewModel.UserId)
      });

      if (foundUser == null)
        throw new BusinessException("User not found.");

      var foundCategory = this._categoryRepository.FindById(new Category()
      {
        Id = Guid.Parse(deleteCategoryViewModel.Id)
      });

      if (foundCategory == null)
        throw new BusinessException("Category not found.");

      var categoryWasCreatedByUser = foundCategory.UserId == foundUser.Id;

      if (!categoryWasCreatedByUser)
        throw new BusinessException("Category not found.");

      this._categoryRepository.Delete(foundCategory);
    }

    public ShowCategoryResponseViewModel GetCategory(
      GetCategoryViewModel getCategoryViewModel
    )
    {
      var foundUser = this._userRepository.FindById(new User()
      {
        Id = Guid.Parse(getCategoryViewModel.UserId)
      });

      if (foundUser == null)
        throw new BusinessException("User not found.");

      var foundCategory = this._categoryRepository.FindById(new Category()
      {
        Id = Guid.Parse(getCategoryViewModel.Id)
      });

      if (foundCategory == null)
        throw new BusinessException("Category not found.");

      var categoryWasCreatedByUser = foundCategory.UserId == foundUser.Id;

      if (!categoryWasCreatedByUser)
        throw new BusinessException("Category not found.");

      return ShowCategoryResponseViewModel.ConvertFromCategory(foundCategory);
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
        .Select(p => ShowCategoryResponseViewModel.ConvertFromCategory(p))
        .ToList();
    }

    public ShowCategoryResponseViewModel UpdateCategory(
      UpdateCategoryViewModel updateCategoryViewModel
    )
    {
      var foundUser = this._userRepository.FindById(new User()
      {
        Id = Guid.Parse(updateCategoryViewModel.UserId)
      });

      if (foundUser == null)
        throw new BusinessException("User not found.");

      var foundCategory = this._categoryRepository.FindById(new Category()
      {
        Id = Guid.Parse(updateCategoryViewModel.Id)
      });

      if (foundCategory == null)
        throw new BusinessException("Category not found.");

      var categoryWasCreatedByUser = foundCategory.UserId == foundUser.Id;

      if (!categoryWasCreatedByUser)
        throw new BusinessException("Category not found.");

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

      foundCategory = this._categoryRepository.Update(foundCategory);

      return ShowCategoryResponseViewModel.ConvertFromCategory(foundCategory);
    }
  }
}
