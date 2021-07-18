using System;
using FinanceOne.Shared.ViewModels;

namespace FinanceOne.Domain.ViewModels.FinancialMovementViewModels
{
  public class GetFinancialMovementViewModel : BaseViewModel
  {
    public string UserId { get; set; }
    public string CategoryId { get; set; }
    public string FinancialMovementId { get; set; }

    public override BaseViewModel DoValidation()
    {
      base.DoValidation();

      if (string.IsNullOrEmpty(this.UserId?.Trim()))
        this._brokenRules.Add("UserId is required.");

      if (!Guid.TryParse(this.UserId, out var parsedGuid))
        this._brokenRules.Add("UserId is not a valid UUID.");

      if (string.IsNullOrEmpty(this.CategoryId?.Trim()))
        this._brokenRules.Add("CategoryId is required.");

      if (!Guid.TryParse(this.CategoryId, out parsedGuid))
        this._brokenRules.Add("CategoryId is not a valid UUID.");

      if (string.IsNullOrEmpty(this.FinancialMovementId?.Trim()))
        this._brokenRules.Add("FinancialMovementId is required.");

      if (!Guid.TryParse(this.FinancialMovementId, out parsedGuid))
        this._brokenRules.Add("FinancialMovementId is not a valid UUID.");

      return this;
    }
  }
}
