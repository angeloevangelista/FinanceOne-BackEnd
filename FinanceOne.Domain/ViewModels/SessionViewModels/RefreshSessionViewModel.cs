using System;
using FinanceOne.Shared.ViewModels;

namespace FinanceOne.Domain.ViewModels.SessionViewModels
{
  public class RefreshSessionViewModel : BaseViewModel
  {
    public string Token { get; set; }
    public string RefreshToken { get; set; }

    public override void DoValidation()
    {
      base.DoValidation();

      if (string.IsNullOrEmpty(this.Token?.Trim()))
        this._brokenRules.Add("Token is required.");

      if (string.IsNullOrEmpty(this.RefreshToken?.Trim()))
        this._brokenRules.Add("RefreshToken token is required.");

      if (!Guid.TryParse(this?.RefreshToken, out var parsedGuid))
        this._brokenRules.Add("RefreshToken is not a valid UUID.");
    }
  }
}
