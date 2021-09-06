using System;
using FinanceOne.Shared.ViewModels;

namespace FinanceOne.Domain.ViewModels.CapitalAmountViewModels
{
  public class ListCapitalAmountsViewModel : BaseViewModel
  {
    public string UserId { get; set; }

    public override BaseViewModel DoValidation()
    {
      base.DoValidation();

      if (string.IsNullOrEmpty(this.UserId?.Trim()))
        this._brokenRules.Add("UserId is required.");

      if (!Guid.TryParse(this.UserId, out var parsedGuid))
        this._brokenRules.Add("UserId is not a valid UUID.");

      return this;
    }
  }
}
