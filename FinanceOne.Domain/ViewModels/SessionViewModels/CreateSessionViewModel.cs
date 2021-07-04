using FinanceOne.Shared.ViewModels;

namespace FinanceOne.Domain.ViewModels.SessionViewModels
{
  public class CreateSessionViewModel : BaseViewModel
  {
    public string Email { get; set; }
    public string Password { get; set; }

    public override void DoValidation()
    {
      base.DoValidation();

      if (string.IsNullOrEmpty(this.Email?.Trim()))
        this._brokenRules.Add("Email is required.");

      if (this.Email?.Length > 50)
        this._brokenRules.Add("Email must be smaller than 50 characters.");

      if (string.IsNullOrEmpty(this.Password?.Trim()))
        this._brokenRules.Add("Password is required.");
    }
  }
}
