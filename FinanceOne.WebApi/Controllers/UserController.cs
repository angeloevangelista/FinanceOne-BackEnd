using Microsoft.AspNetCore.Mvc;
using FinanceOne.Domain.Services;
using FinanceOne.Domain.ViewModels.UserViewModels;
using FinanceOne.Shared.Controllers;
using Microsoft.AspNetCore.Authorization;
using FinanceOne.Shared.Contracts.Services;
using FinanceOne.Domain.Entities;
using System;
using Microsoft.AspNetCore.Http;

namespace FinanceOne.WebApi.Controllers
{
  [ApiController]
  public class UserController : CustomControllerBase
  {
    private readonly IUserService _userService;

    public UserController(IUserService userService, IJwtService jwtService)
      : base(jwtService)
    {
      this._userService = userService;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("/v1/users")]
    public ActionResult<ShowUserResponseViewModel> CreateUser(
      [FromBody] CreateUserViewModel createUserViewModel
    )
    {
      ValidateViewModel(createUserViewModel);

      var createdUser = this._userService.CreateUser(createUserViewModel);

      return Ok(createdUser);
    }

    [HttpPut]
    [Route("/v1/users")]
    public ActionResult<ShowUserResponseViewModel> UpdateUser(
      [FromBody] UpdateUserViewModel updateUserViewModel
    )
    {
      updateUserViewModel.Id = this.GetTokenPayload<SessionPayload>().Id;

      ValidateViewModel(updateUserViewModel);

      var updatedUser = this._userService.UpdateUser(updateUserViewModel);

      return Ok(updatedUser);
    }

    [HttpPatch]
    [Route("/v1/users")]
    public ActionResult<ShowUserResponseViewModel> UpdateAvatar(
      [FromForm] UpdateAvatarViewModel updateAvatarViewModel
    )
    {
      updateAvatarViewModel.UserId = this.GetTokenPayload<SessionPayload>().Id;

      ValidateViewModel(updateAvatarViewModel);

      var updatedUser = this._userService.UpdateAvatar(updateAvatarViewModel);

      return Ok(updatedUser);
    }

    [HttpDelete]
    [Route("/v1/users")]
    public ActionResult DeleteUser()
    {
      var userId = this.GetTokenPayload<SessionPayload>().Id;

      this._userService.DeleteUser(userId);

      return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpGet]
    [Route("/v1/users")]
    public ActionResult<ShowUserResponseViewModel> GetUser()
    {
      var userId = this.GetTokenPayload<SessionPayload>().Id;

      var user = this._userService.GetUser(userId);

      return Ok(user);
    }
  }
}
