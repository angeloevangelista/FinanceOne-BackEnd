using System;
using FinanceOne.Shared.ViewModels;

namespace FinanceOne.Domain.ViewModels.CapitalAmountViewModels
{
  public class DeleteCapitalAmountViewModel : BaseViewModel
  {
    public string Id { get; set; }
    public string UserId { get; set; }

    public override BaseViewModel DoValidation()
    {
      base.DoValidation();

      if (string.IsNullOrEmpty(this.Id?.Trim()))
        this._brokenRules.Add("Id is required.");

      if (!Guid.TryParse(this?.Id, out var parsedGuid))
        this._brokenRules.Add("Id is not a valid UUID.");

      if (string.IsNullOrEmpty(this.UserId?.Trim()))
        this._brokenRules.Add("UserId is required.");

      if (!Guid.TryParse(this.UserId, out parsedGuid))
        this._brokenRules.Add("UserId is not a valid UUID.");

      return this;
    }
  }
}
