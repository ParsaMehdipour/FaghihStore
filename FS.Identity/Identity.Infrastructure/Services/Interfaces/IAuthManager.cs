using User.Application.Users.Commands.RegisterUser;
using User.Application.Users.Queries.DetailUser;
using User.Domain.Models;

namespace Identity.Infrastructure.Services.Interfaces;

public interface IAuthManager
{
    Task<Result> Register(RegisterUserCommand command, CancellationToken cancellationToken);
    Task<Result> VerifyAndSignInUser(VerificationCodeDto dto, CancellationToken cancellationToken);
    Result VerifyCode(VerificationCodeDto dto);
    Task<Result> Signin(ApplicationUser user, bool isPersistent, CancellationToken cancellationToken);
    Task<Result> SignOut();
    Task<Result> UpgradeUserRoleToAdmin(Guid id, CancellationToken cancellationToken);
    Task<Result<DetailUserDto>> CurrentUser(CancellationToken cancellationToken);
}