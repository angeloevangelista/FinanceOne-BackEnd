using System;
using FinanceOne.Domain.Enumerators;
using FinanceOne.Shared.ViewModels;
using FinanceOne.Shared.Util.DataTypes;
using FinanceOne.Shared.Enumerators;

namespace FinanceOne.Domain.ViewModels.FinancialMovementViewModels
{
  public class CreateFinancialMovementViewModel : BaseViewModel
  {
    public string Name { get; set; }
    public decimal Cost { get; set; }
    public string Description { get; set; }
    public long Amount { get; set; }
    public string Paid { get; set; }
    public string UserId { get; set; }
    public string CategoryId { get; set; }
    public string FinancialMovementType { get; set; }

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

      if (string.IsNullOrEmpty(this.Name?.Trim()))
        this._brokenRules.Add("Name is required.");

      if (this.Name?.Length > 50)
        this._brokenRules.Add("Name must be smaller than 50 characters.");

      if (this.Cost <= 0)
        this._brokenRules.Add("Cost must be a positive value.");

      if (this.Amount <= 0)
        this._brokenRules.Add("Amount must be a positive value.");

      if (this.Paid == null)
        this._brokenRules.Add("Paid is required.");

      if (!UtilEnum.TryParse<IndicatorYesNo>(
        this.Paid,
        out var parsedEnumValueA
      ))
        this._brokenRules.Add("Paid is invalid.");

      if (this.FinancialMovementType == null)
        this._brokenRules.Add("FinancialMovementType is required.");

      if (!UtilEnum.TryParse<FinancialMovementType>(
        this.FinancialMovementType,
        out var parsedEnumValueB
      ))
        this._brokenRules.Add("FinancialMovementType is invalid.");

      return this;
    }
  }
}
