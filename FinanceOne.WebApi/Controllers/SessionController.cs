using Microsoft.AspNetCore.Mvc;
using FinanceOne.Domain.Services;
using FinanceOne.Domain.ViewModels.SessionViewModels;
using FinanceOne.Shared.Controllers;
using FinanceOne.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using FinanceOne.Shared.Contracts.Services;

namespace FinanceOne.WebApi.Controllers
{
  [ApiController]
  public class SessionController : CustomControllerBase
  {
    private readonly ISessionService _sessionService;

    public SessionController(
      ISessionService sessionService,
      IJwtService jwtService
    ) : base(jwtService)
    {
      this._sessionService = sessionService;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("/v1/sessions")]
    public ActionResult<ApiResponse<ResultSessionViewModel>> CreateSession(
      [FromBody] CreateSessionViewModel createSessionViewModel
    )
    {
      ValidateViewModel(createSessionViewModel);

      var session = this._sessionService.CreateSession(
        createSessionViewModel
      );

      return Ok(new ApiResponse<ResultSessionViewModel>(session));
    }

    [HttpPut]
    [AllowAnonymous]
    [Route("/v1/sessions")]
    public ActionResult<ApiResponse<ResultSessionViewModel>> RefreshSession(
      [FromBody] RefreshSessionViewModel refreshSessionViewModel
    )
    {
      refreshSessionViewModel.Token = this.GetAuthorizationToken();

      ValidateViewModel(refreshSessionViewModel);

      var session = this._sessionService.RefreshSession(
        refreshSessionViewModel
      );

      return Ok(new ApiResponse<ResultSessionViewModel>(session));
    }
  }
}
