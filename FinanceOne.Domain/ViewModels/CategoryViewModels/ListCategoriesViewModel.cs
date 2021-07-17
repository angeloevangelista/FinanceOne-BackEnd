using System;
using FinanceOne.Shared.ViewModels;

namespace FinanceOne.Domain.ViewModels.CategoryViewModels
{
  public class ListCategoriesViewModel : BaseViewModel
  {
    public string UserId { get; set; }

    public override BaseViewModel DoValidation()
    {
      base.DoValidation();

      if (string.IsNullOrEmpty(this.UserId?.Trim()))
        this._brokenRules.Add("Id is required.");

      if (!Guid.TryParse(this.UserId, out var parsedGuid))
        this._brokenRules.Add("Id is not a valid UUID.");

      return this;
    }
  }
}
