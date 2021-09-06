using System;
using System.Collections.Generic;
using System.Linq;
using FinanceOne.Domain.Entities;
using FinanceOne.Domain.Enumerators;
using FinanceOne.Domain.Services;
using FinanceOne.Domain.ViewModels.CategoryViewModels;
using FinanceOne.Domain.ViewModels.FinancialMovementViewModels;
using FinanceOne.Shared.Enumerators;
using FinanceOne.Shared.Exceptions;
using FinanceOne.Shared.Repositories;
using FinanceOne.Shared.Util.DataTypes;

namespace FinanceOne.Implementation.Services
{
  public class FinancialMovementService : IFinancialMovementService
  {
    private readonly IUserRepository _userRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IFinancialMovementRepository _financialMovementRepository;

    public FinancialMovementService(
      IUserRepository userRepository,
      ICategoryRepository categoryRepository,
      IFinancialMovementRepository financialMovementRepository
    )
    {
      this._categoryRepository = categoryRepository;
      this._userRepository = userRepository;
      this._financialMovementRepository = financialMovementRepository;
    }

    public ShowFinancialMovementResponseViewModel CreateFinancialMovement(
      CreateFinancialMovementViewModel createFinancialMovementViewModel
    )
    {
      var foundCategory = ICategoryService.ValidateCategoryWasCreatedByUser(
        this._categoryRepository,
        this._userRepository,
        createFinancialMovementViewModel.CategoryId,
        createFinancialMovementViewModel.UserId
      );

      var financialMovement = new FinancialMovement()
      {
        Id = Guid.NewGuid(),
        CategoryId = foundCategory.Id,
        Name = createFinancialMovementViewModel.Name,
        Description = createFinancialMovementViewModel.Description,
        Amount = createFinancialMovementViewModel.Amount,
        Cost = createFinancialMovementViewModel.Cost,
        Paid = UtilEnum.Parse<IndicatorYesNo>(
          createFinancialMovementViewModel.Paid
        ),
        FinancialMovementType = UtilEnum.Parse<FinancialMovementType>(
          createFinancialMovementViewModel.FinancialMovementType
        ),
      };

      financialMovement = this._financialMovementRepository.Create(
        financialMovement
      );

      return ShowFinancialMovementResponseViewModel.ConvertFromEntity(
        financialMovement
      );
    }

    public void DeleteFinancialMovement(
      DeleteFinancialMovementViewModel deleteFinancialMovementViewModel
    )
    {
      var foundCategory = ICategoryService.ValidateCategoryWasCreatedByUser(
        this._categoryRepository,
        this._userRepository,
        deleteFinancialMovementViewModel.CategoryId,
        deleteFinancialMovementViewModel.UserId
      );

      var foundFinancialMovement = this._financialMovementRepository.FindById(
        new FinancialMovement()
        {
          Id = Guid.Parse(deleteFinancialMovementViewModel.FinancialMovementId)
        }
      );

      if (foundFinancialMovement == null)
        throw new BusinessException("Financial movement not found.");

      foundFinancialMovement.UpdatedAt = DateTime.UtcNow;

      this._financialMovementRepository.Delete(foundFinancialMovement);
    }

    public ShowFinancialMovementResponseViewModel GetFinancialMovement(
      GetFinancialMovementViewModel getFinancialMovementViewModel
    )
    {
      var foundCategory = ICategoryService.ValidateCategoryWasCreatedByUser(
        this._categoryRepository,
        this._userRepository,
        getFinancialMovementViewModel.CategoryId,
        getFinancialMovementViewModel.UserId
      );

      var financialMovement = this._financialMovementRepository.FindById(
        new FinancialMovement()
        {
          Id = Guid.Parse(getFinancialMovementViewModel.FinancialMovementId)
        }
      );

      return ShowFinancialMovementResponseViewModel.ConvertFromEntity(
        financialMovement
      );
    }

    public IList<ShowFinancialMovementResponseViewModel> ListFinancialMovements(
      ListFinancialMovementsViewModel listFinancialMovementsViewModel
    )
    {
      var foundCategory = ICategoryService.ValidateCategoryWasCreatedByUser(
        this._categoryRepository,
        this._userRepository,
        listFinancialMovementsViewModel.CategoryId,
        listFinancialMovementsViewModel.UserId
      );

      var financialMovements = this._financialMovementRepository
        .ListByCategory(new FinancialMovement()
        {
          CategoryId = foundCategory.Id
        });

      return financialMovements
        .Select(p =>
          ShowFinancialMovementResponseViewModel.ConvertFromEntity(p)
        )
        .ToList();
    }

    public IList<ShowCategoryResponseViewModel> ListFinancialMovementsByUser(
      ListFinancialMovementsByUserViewModel listFinancialMovementsByUserViewModel
    )
    {
      var categories = this._categoryRepository
        .ListByUserIncludingFinancialMovements(new Category()
        {
          Active = IndicatorYesNo.Yes,
          UserId = Guid.Parse(listFinancialMovementsByUserViewModel.UserId)
        });

      var categoriesResponse = categories
        .Select(p =>
          ShowCategoryResponseViewModel.ConvertFromEntity(p)
        )
        .ToList();

      return categoriesResponse;
    }

    public ShowFinancialMovementResponseViewModel UpdateFinancialMovement(
      UpdateFinancialMovementViewModel updateFinancialMovementViewModel
    )
    {
      ICategoryService.ValidateCategoryWasCreatedByUser(
        this._categoryRepository,
        this._userRepository,
        updateFinancialMovementViewModel.CategoryId,
        updateFinancialMovementViewModel.UserId
      );

      var foundFinancialMovement = this._financialMovementRepository.FindById(
        new FinancialMovement()
        {
          Id = Guid.Parse(updateFinancialMovementViewModel.Id)
        }
      );

      if (foundFinancialMovement == null)
        throw new BusinessException("Financial movement not found.");

      foundFinancialMovement.Name = updateFinancialMovementViewModel.Name;
      foundFinancialMovement.Amount = updateFinancialMovementViewModel.Amount;
      foundFinancialMovement.Name = updateFinancialMovementViewModel.Name;
      foundFinancialMovement.Cost = updateFinancialMovementViewModel.Cost;
      foundFinancialMovement.UpdatedAt = DateTime.UtcNow;

      foundFinancialMovement.Description =
        updateFinancialMovementViewModel.Description;

      foundFinancialMovement.Paid =
        UtilEnum.Parse<IndicatorYesNo>(
          updateFinancialMovementViewModel.Paid
        );

      foundFinancialMovement.FinancialMovementType =
        UtilEnum.Parse<FinancialMovementType>(
          updateFinancialMovementViewModel.FinancialMovementType
        );

      foundFinancialMovement = this._financialMovementRepository.Update(
        foundFinancialMovement
      );

      return ShowFinancialMovementResponseViewModel.ConvertFromEntity(
        foundFinancialMovement
      );
    }
  }
}
