using System;
using System.Linq;
using FinanceOne.Domain.Entities;
using FinanceOne.Shared.Enumerators;
using FinanceOne.Shared.Exceptions;
using FinanceOne.Shared.Repositories;

namespace FinanceOne.Domain.Services
{
  struct MonthYearPair
  {
    public int Month { get; set; }
    public int Year { get; set; }
  }

  public interface ICapitalAmountServiceStaticMembers
  {
    static CapitalAmount ValidateCapitalAmountBelongsToUser(
      ICapitalAmountRepository capitalAmountRepository,
      IUserRepository userRepository,
      string capitalAmountId,
      string userId
    )
    {
      var foundUser = userRepository.FindById(new User()
      {
        Id = Guid.Parse(userId)
      });

      if (foundUser == null)
        throw new BusinessException("User not found.");

      var foundCapitalAmount = capitalAmountRepository.FindById(
        new CapitalAmount()
        {
          Id = Guid.Parse(capitalAmountId)
        }
      );

      if (foundCapitalAmount == null)
        throw new BusinessException("Capital amount not found.");

      var capitalAmountWasCreatedByUser =
        foundCapitalAmount.UserId == foundUser.Id;

      if (!capitalAmountWasCreatedByUser)
        throw new BusinessException("Capital amount not found.");

      return foundCapitalAmount;
    }

    static void ValidateReferenceDateIsAlreadyRegistered(
      ICapitalAmountRepository capitalAmountRepository,
      IUserRepository userRepository,
      string userId,
      DateTime referenceDateToCheck
    )
    {
      var foundUser = userRepository.FindById(new User()
      {
        Id = Guid.Parse(userId)
      });

      if (foundUser == null)
        throw new BusinessException("User not found.");

      var capitalAmounts = capitalAmountRepository.ListByUser(
        new CapitalAmount()
        {
          UserId = foundUser.Id,
          Active = IndicatorYesNo.Yes
        }
      );

      (int month, int year) newPairDate = (
        referenceDateToCheck.Month,
        referenceDateToCheck.Year
      );

      var monthlyAmountAlreadyRegistered = capitalAmounts
        .Any(p =>
        {
          (int month, int year) existingPairDate = (
            p.ReferenceDate.Month,
            p.ReferenceDate.Year
          );

          return existingPairDate == newPairDate;
        });

      if (monthlyAmountAlreadyRegistered)
        throw new BusinessException("Monthly amount already registered.");
    }
  }
}
