using System;
using FinanceOne.Shared.ViewModels;

namespace FinanceOne.Domain.ViewModels.CategoryViewModels
{
  public class UpdateCategoryViewModel : BaseViewModel
  {
    public string UserId { get; set; }
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

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

      if (string.IsNullOrEmpty(this.Name?.Trim()))
        this._brokenRules.Add("Name is required.");

      if (this.Name?.Length > 50)
        this._brokenRules.Add("Name must be smaller than 50 characters.");

      if (string.IsNullOrEmpty(this.Description?.Trim()))
        this._brokenRules.Add("Description is required.");

      if (this.Description?.Length > 150)
        this._brokenRules.Add("Description must be smaller than 150 characters.");

      return this;
    }
  }
}
