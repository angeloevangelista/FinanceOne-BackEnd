using System;
using System.Collections.Generic;
using FinanceOne.Domain.Entities;
using FinanceOne.Domain.ViewModels.CategoryViewModels;
using FinanceOne.Shared.Exceptions;
using FinanceOne.Shared.Repositories;

namespace FinanceOne.Domain.Services
{
  public interface ICategoryServiceStaticMembers
  {
    static Category ValidateCategoryWasCreatedByUser(
      ICategoryRepository categoryRepository,
      IUserRepository userRepository,
      string categoryId,
      string userId
    )
    {
      var foundUser = userRepository.FindById(new User()
      {
        Id = Guid.Parse(userId)
      });

      if (foundUser == null)
        throw new BusinessException("User not found.");

      var foundCategory = categoryRepository.FindById(new Category()
      {
        Id = Guid.Parse(categoryId)
      });

      if (foundCategory == null)
        throw new BusinessException("Category not found.");

      var categoryWasCreatedByUser = foundCategory.UserId == foundUser.Id;

      if (!categoryWasCreatedByUser)
        throw new BusinessException("Category not found.");

      return foundCategory;
    }
  }
}
