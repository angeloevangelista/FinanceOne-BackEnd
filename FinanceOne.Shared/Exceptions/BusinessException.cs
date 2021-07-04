using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceOne.Shared.Exceptions
{
  public class BusinessException : Exception
  {
    private readonly IList<string> _brokenRules;

    public IReadOnlyCollection<string> BrokenRules
    {
      get => this._brokenRules.ToArray();
    }

    public BusinessException()
    {
      this._brokenRules = new List<string>();
    }

    public BusinessException(params string[] brokenRules) : this()
    {
      foreach (var brokenRule in brokenRules)
        this.AddBrokenRule(brokenRule);
    }

    public BusinessException AddBrokenRule(string brokenRule)
    {
      this._brokenRules.Add(brokenRule);

      return this;
    }
  }
}
