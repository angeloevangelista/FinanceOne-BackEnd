using System;
using FinanceOne.Shared.ViewModels;

namespace FinanceOne.Domain.ViewModels.UserViewModels
{
  public class UpdateUserViewModel : BaseViewModel
  {
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public override void DoValidation()
    {
      base.DoValidation();

      if (string.IsNullOrEmpty(this.Id?.Trim()))
        this._brokenRules.Add("Id is required.");

      if (!Guid.TryParse(this?.Id, out var parsedGuid))
        this._brokenRules.Add("Id is not a valid UUID.");

      if (string.IsNullOrEmpty(this.FirstName?.Trim()))
        this._brokenRules.Add("FirstName is required.");

      if (this.FirstName?.Length > 50)
        this._brokenRules.Add("FirstName must be smaller than 50 characters.");

      if (string.IsNullOrEmpty(this.LastName?.Trim()))
        this._brokenRules.Add("LastName is required.");

      if (this.LastName?.Length > 50)
        this._brokenRules.Add("LastName must be smaller than 50 characters.");

      if (string.IsNullOrEmpty(this.Email?.Trim()))
        this._brokenRules.Add("Email is required.");

      if (this.Email?.Length > 50)
        this._brokenRules.Add("Email must be smaller than 50 characters.");

      var isUpdatingPassword = !string.IsNullOrEmpty(this.Password?.Trim());

      if (isUpdatingPassword)
      {
        if (this.Password?.Length < 8)
          this._brokenRules.Add("Password must be larger than 8 characters.");

        if (this.Password?.Length > 16)
          this._brokenRules.Add("Password must be smaller than 16 characters.");
      }
    }
  }
}
