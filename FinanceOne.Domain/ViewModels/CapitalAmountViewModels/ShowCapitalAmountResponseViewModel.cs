using System;
using FinanceOne.Domain.Entities;
using FinanceOne.Shared.ViewModels;

namespace FinanceOne.Domain.ViewModels.CapitalAmountViewModels
{
  public class ShowCapitalAmountResponseViewModel : BaseViewModel
  {
    public string Id { get; set; }
    public string UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public decimal Amount { get; set; }
    public DateTime ReferenceDate { get; set; }

    public static ShowCapitalAmountResponseViewModel ConvertFromEntity(
      CapitalAmount capitalAmount
    ) => new ShowCapitalAmountResponseViewModel()
    {
      Id = capitalAmount.Id.ToString(),
      UserId = capitalAmount.UserId.ToString(),
      CreatedAt = capitalAmount.CreatedAt,
      UpdatedAt = capitalAmount.UpdatedAt,
      Amount = capitalAmount.Amount,
      ReferenceDate = capitalAmount.ReferenceDate
    };
  }
}
