using System;
using System.Collections.Generic;
using FinanceOne.Shared.ViewModels;

namespace FinanceOne.Domain.ViewModels.CapitalAmountViewModels
{
  public class CreateCapitalAmountViewModel : BaseViewModel
  {
    public decimal Amount { get; set; }
    public string UserId { get; set; }
    public DateTime? ReferenceDate { get; set; }

    public override BaseViewModel DoValidation()
    {
      base.DoValidation();

      if (string.IsNullOrEmpty(this.UserId?.Trim()))
        this._brokenRules.Add("UserId is required.");

      if (!Guid.TryParse(this.UserId, out var parsedGuid))
        this._brokenRules.Add("UserId is not a valid UUID.");

      if (this.ReferenceDate == null)
        this._brokenRules.Add("ReferenceDate is required.");

      return this;
    }
  }
}
