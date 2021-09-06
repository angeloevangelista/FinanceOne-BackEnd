using System;
using System.Collections.Generic;
using System.Linq;
using FinanceOne.Domain.Entities;
using FinanceOne.Domain.Services;
using FinanceOne.Domain.ViewModels.CapitalAmountViewModels;
using FinanceOne.Shared.Enumerators;
using FinanceOne.Shared.Exceptions;
using FinanceOne.Shared.Repositories;

namespace FinanceOne.Implementation.Services
{
  public class CapitalAmountService : ICapitalAmountService
  {
    private readonly IUserRepository _userRepository;
    private readonly ICapitalAmountRepository _capitalAmountRepository;

    public CapitalAmountService(
      IUserRepository userRepository,
      ICapitalAmountRepository capitalAmountRepository
    )
    {
      this._capitalAmountRepository = capitalAmountRepository;
      this._userRepository = userRepository;
    }

    public ShowCapitalAmountResponseViewModel CreateCapitalAmount(
      CreateCapitalAmountViewModel createCapitalAmountViewModel
    )
    {
      var foundUser = this._userRepository.FindById(new User()
      {
        Id = Guid.Parse(createCapitalAmountViewModel.UserId)
      });

      if (foundUser == null)
        throw new BusinessException("User not found.");

      var capitalAmount = new CapitalAmount()
      {
        Id = Guid.NewGuid(),
        UserId = foundUser.Id,
        Active = IndicatorYesNo.Yes,
        Amount = createCapitalAmountViewModel.Amount,
        ReferenceDate = createCapitalAmountViewModel.ReferenceDate
          .GetValueOrDefault(),
      };

      ICapitalAmountService.ValidateReferenceDateIsAlreadyRegistered(
        _capitalAmountRepository,
        _userRepository,
        createCapitalAmountViewModel.UserId,
        createCapitalAmountViewModel.ReferenceDate.GetValueOrDefault()
      );

      capitalAmount = this._capitalAmountRepository.Create(capitalAmount);

      return ShowCapitalAmountResponseViewModel.ConvertFromEntity(capitalAmount);
    }

    public void DeleteCapitalAmount(
      DeleteCapitalAmountViewModel deleteCapitalAmountViewModel
    )
    {
      var foundCapitalAmount = ICapitalAmountService
        .ValidateCapitalAmountBelongsToUser(
          this._capitalAmountRepository,
          this._userRepository,
          deleteCapitalAmountViewModel.Id,
          deleteCapitalAmountViewModel.UserId
        );

      foundCapitalAmount.UpdatedAt = DateTime.UtcNow;

      this._capitalAmountRepository.Delete(foundCapitalAmount);
    }

    public IList<ShowCapitalAmountResponseViewModel> ListCapitalAmounts(
      ListCapitalAmountsViewModel listCapitalAmountsViewModel
    )
    {
      var foundUser = this._userRepository.FindById(new User()
      {
        Id = Guid.Parse(listCapitalAmountsViewModel.UserId)
      });

      if (foundUser == null)
        throw new BusinessException("User not found.");

      var categories = this._capitalAmountRepository.ListByUser(
        new CapitalAmount()
        {
          UserId = foundUser.Id
        }
      );

      return categories
        .Select(p => ShowCapitalAmountResponseViewModel.ConvertFromEntity(p))
        .ToList();
    }

    public ShowCapitalAmountResponseViewModel UpdateCapitalAmount(
      UpdateCapitalAmountViewModel updateCapitalAmountViewModel
    )
    {
      var foundCapitalAmount = ICapitalAmountService
        .ValidateCapitalAmountBelongsToUser(
          this._capitalAmountRepository,
          this._userRepository,
          updateCapitalAmountViewModel.Id,
          updateCapitalAmountViewModel.UserId
        );

      var updateReferenceDate =
        updateCapitalAmountViewModel.ReferenceDate.Date != foundCapitalAmount.ReferenceDate.Date;

      if (updateReferenceDate)
      {
        ICapitalAmountService.ValidateReferenceDateIsAlreadyRegistered(
          _capitalAmountRepository,
          _userRepository,
          updateCapitalAmountViewModel.UserId,
          updateCapitalAmountViewModel.ReferenceDate
        );

        foundCapitalAmount
          .ReferenceDate = updateCapitalAmountViewModel.ReferenceDate;
      }

      foundCapitalAmount.Amount = updateCapitalAmountViewModel.Amount;
      foundCapitalAmount.UpdatedAt = DateTime.Now;

      foundCapitalAmount = this._capitalAmountRepository.Update(
        foundCapitalAmount
      );

      return ShowCapitalAmountResponseViewModel.ConvertFromEntity(
        foundCapitalAmount
      );
    }
  }
}
