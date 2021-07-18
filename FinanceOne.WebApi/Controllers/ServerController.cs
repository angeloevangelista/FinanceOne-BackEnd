using FinanceOne.Shared.Contracts.Services;
using FinanceOne.Shared.Controllers;
using FinanceOne.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceOne.WebApi.Controllers
{
  public class ServerController : CustomControllerBase
  {
    public ServerController(IJwtService jwtService) : base(jwtService)
    {
    }

    [Route("{*url}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [AllowAnonymous]
    public ActionResult CatchAll()
    {
      Response.StatusCode = 404;

      return base.NotFound(new ApiResponse<object>(new { Message = "Uhuuul! You are in a void" }));
    }
  }
}
