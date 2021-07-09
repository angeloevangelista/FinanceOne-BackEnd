using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceOne.Shared.Exceptions
{
  public class AuthException : CustomException
  {
    public AuthException(params string[] brokenRules) : base(brokenRules)
    {
    }
  }
}
