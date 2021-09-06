using System;
using FinanceOne.Shared.ViewModels;

namespace FinanceOne.Domain.ViewModels.CapitalAmountViewModels
{
  public class UpdateCapitalAmountViewModel : BaseViewModel
  {
    public string UserId { get; set; }
    public string Id { get; set; }
    public DateTime ReferenceDate { get; set; }
    public decimal Amount { get; set; }

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

      if (this.ReferenceDate == DateTime.MinValue)
        this._brokenRules.Add("ReferenceDate is required.");

      if (this.Amount == 0)
        this._brokenRules.Add("Amount must not be zero.");

      return this;
    }
  }
}
