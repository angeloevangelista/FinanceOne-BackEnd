using System.Collections.Generic;
using FinanceOne.Domain.ViewModels.CapitalAmountViewModels;

namespace FinanceOne.Domain.Services
{
  public interface ICapitalAmountService: ICapitalAmountServiceStaticMembers
  {
    ShowCapitalAmountResponseViewModel CreateCapitalAmount(
      CreateCapitalAmountViewModel createCapitalAmountViewModel
    );

    IList<ShowCapitalAmountResponseViewModel> ListCapitalAmounts(
      ListCapitalAmountsViewModel listCapitalAmountsViewModel
    );

    ShowCapitalAmountResponseViewModel UpdateCapitalAmount(
      UpdateCapitalAmountViewModel updateCapitalAmountViewModel
    );

    void DeleteCapitalAmount(
      DeleteCapitalAmountViewModel deleteCapitalAmountViewModel
    );
  }
}
