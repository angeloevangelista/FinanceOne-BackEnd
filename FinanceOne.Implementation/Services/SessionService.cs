using FinanceOne.Domain.Entities;
using FinanceOne.Domain.Services;
using FinanceOne.Shared.Exceptions;
using FinanceOne.Shared.Repositories;
using FinanceOne.Domain.ViewModels.SessionViewModels;
using FinanceOne.Shared.Contracts.Services;

namespace FinanceOne.Implementation.Services
{
  public class SessionService : ISessionService
  {
    private readonly IHashService _hashService;
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public SessionService(
      IJwtService jwtService,
      IHashService hashService,
      IUserRepository userRepository
    )
    {
      this._jwtService = jwtService;
      this._hashService = hashService;
      this._userRepository = userRepository;
    }

    public string CreateSession(CreateSessionViewModel createSessionViewModel)
    {
      var foundUser = this._userRepository.FindByEmail(
        createSessionViewModel.Email
      );

      if (foundUser == null)
        throw new BusinessException("Invalid credentials.");

      var passwordsMatch = this._hashService.Verify(
        createSessionViewModel.Password,
        foundUser.Password
      );

      if (!passwordsMatch)
        throw new BusinessException("Invalid credentials.");

      var sessionPayload = new SessionPayload()
      {
        Id = foundUser.Id.ToString(),
        Name = $"{foundUser.FirstName} {foundUser.LastName}",
        Email = foundUser.Email,
      };

      var token = this._jwtService.GenerateToken(sessionPayload, "Id");

      return token;
    }
  }
}
