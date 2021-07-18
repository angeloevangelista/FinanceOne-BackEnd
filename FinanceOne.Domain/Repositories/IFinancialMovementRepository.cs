using System.Collections.Generic;
using FinanceOne.Domain.Entities;

namespace FinanceOne.Shared.Repositories
{
  public interface IFinancialMovementRepository
  {
    FinancialMovement Create(FinancialMovement financialMovement);
    FinancialMovement Update(FinancialMovement financialMovement);
    void Delete(FinancialMovement financialMovement);
    FinancialMovement FindById(FinancialMovement financialMovement);

    IList<FinancialMovement> ListByCategory(
      FinancialMovement financialMovement
    );
  }
}
