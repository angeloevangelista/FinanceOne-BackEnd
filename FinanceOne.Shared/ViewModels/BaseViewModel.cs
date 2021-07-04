using System.Collections.Generic;
using System.Linq;

namespace FinanceOne.Shared.ViewModels
{
  public abstract class BaseViewModel
  {
    protected IList<string> _brokenRules;

    public bool IsValid
    {
      get
      {
        this.DoValidation();

        return !this._brokenRules.Any();
      }
    }

    public bool IsInvalid { get => !this.IsValid; }

    public BaseViewModel()
    {
      this._brokenRules = new List<string>();
    }

    public virtual void DoValidation()
    {
      this._brokenRules = new List<string>();
    }

    public IList<string> GetBrokenRules() => this._brokenRules.ToArray();
  }
}
