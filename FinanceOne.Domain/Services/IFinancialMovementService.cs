using System.Collections.Generic;
using FinanceOne.Domain.ViewModels.FinancialMovementViewModels;

namespace FinanceOne.Domain.Services
{
  public interface IFinancialMovementService
  {
    ShowFinancialMovementResponseViewModel CreateFinancialMovement(
      CreateFinancialMovementViewModel createFinancialMovementViewModel
    );

    ShowFinancialMovementResponseViewModel UpdateFinancialMovement(
      UpdateFinancialMovementViewModel updateFinancialMovementViewModel
    );

    IList<ShowFinancialMovementResponseViewModel> ListFinancialMovements(
      ListFinancialMovementsViewModel listFinancialMovementsViewModel
    );

    ShowFinancialMovementResponseViewModel GetFinancialMovement(
      GetFinancialMovementViewModel getFinancialMovementViewModel
    );

    void DeleteFinancialMovement(
      DeleteFinancialMovementViewModel deleteFinancialMovementViewModel
    );
  }
}
