using Identity.Infrastructure.Services.Interfaces;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Role.Domain.Repositories;

using SH.Application.Interfaces;
using SH.Infrastructure.Extensions;

using System.Security.Claims;

using User.Application.Users.Commands.RegisterUser;
using User.Application.Users.Queries.DetailUser;
using User.Domain.Models;
using User.Domain.Repositories;

namespace Identity.Infrastructure.Services;

public class AuthManager : IAuthManager
{
    protected IMediator _mediator { get; }
    protected ISmsService _smsService { get; }
    protected ICookieHelper _cookieHelper { get; }
    protected SignInManager<ApplicationUser> _signInManager { get; }
    protected ILogger<AuthManager> _logger { get; }
    protected IUserRepository _userRepository { get; }
    protected IRoleRepository _roleRepository { get; }

    public AuthManager(IMediator mediator,
        ISmsService smsService,
        ICookieHelper cookieHelper,
        SignInManager<ApplicationUser> signInManager,
        ILogger<AuthManager> logger,
        IUserRepository userRepository,
        IRoleRepository roleRepository)
    {
        _mediator = mediator;
        _smsService = smsService;
        _cookieHelper = cookieHelper;
        _signInManager = signInManager;
        _logger = logger;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task<Result> Register(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var registerResult = await _mediator.Send(command, cancellationToken);
        if (registerResult.IsFailed)
            return Result.Fail(registerResult.Errors.Select(_ => _.Message).FirstOrDefault());

        var user = registerResult.Value;

        var sendActivationCode = await _smsService.SendActivationCodeAsync(user.PhoneNumber, cancellationToken);

        if (sendActivationCode.IsFailed)
            return Result.Fail(sendActivationCode.Errors.FirstOrDefault()); //TODO: we must be check this

        return Result.Ok();
    }

    public async Task<Result> VerifyAndSignInUser(VerificationCodeDto dto, CancellationToken cancellationToken)
    {
        ApplicationUser user =
            await _signInManager.UserManager.Users.AsNoTracking().SingleOrDefaultAsync(_ => _.UserName.Equals(dto.PhoneNumber),
                cancellationToken);

        if (user is null)
            return Result.Fail(ValidationMessage.UserNotFound);

        var verificationCode = VerifyCode(dto);

        if (verificationCode.IsFailed)
            return Result.Fail(verificationCode.Errors.Select(_ => _.Message).FirstOrDefault());

        user.PhoneNumberConfirmed = true;

        var userSigninResult = await Signin(user, true, cancellationToken);

        if (userSigninResult.IsFailed)
            return Result.Fail(userSigninResult.Errors.Select(_ => _.Message).FirstOrDefault());

        _cookieHelper.Delete(IdentityConsts.CookieActivationCodeKey);

        return Result.Ok();
    }

    public Result VerifyCode(VerificationCodeDto dto)
    {
        var value = _cookieHelper.Get(IdentityConsts.CookieActivationCodeKey);
        if (string.IsNullOrWhiteSpace(value))
        {
            _logger.LogWarning($"Verification Code is wrong");

            return Result.Fail(ValidationMessage.VerificationCodeIsInvalid);
        }

        string phoneNumber = value.Split("-").FirstOrDefault();
        string code = value.Split("-").LastOrDefault();

        if ((dto.PhoneNumber.Equals(phoneNumber) && dto.VerificationCode.Equals(code)) is false)
            return Result.Fail(ValidationMessage.VerificationCodeIsInvalid);

        return Result.Ok();
    }

    public async Task<Result> Signin(ApplicationUser user, bool isPersistent, CancellationToken cancellationToken)
    {
        var userCanSignIn = await _signInManager.CanSignInAsync(user);
        if (userCanSignIn is false)
        {
            _logger.LogWarning($"User {user.UserName} Cannot Signin!");

            return Result.Fail(ValidationMessage.UserCannotSignin);
        }

        //await _signInManager.Context.SignInAsync(new IdentityServerUser(user.Id.ToString()) { AdditionalClaims = claims }, new() { IsPersistent = isPersistent });

        await _signInManager.SignInAsync(user, null);

        _logger.LogInformation($"User {user.UserName} Successfully SignedIn");

        return Result.Ok();
    }

    public async Task<Result> SignOut()
    {
        await _signInManager.SignOutAsync();

        return Result.Ok();
    }

    public async Task<Result> UpgradeUserRoleToAdmin(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUsersByRole(IdentityConsts.ROLE_USER).SingleOrDefaultAsync(_ => _.Id.Equals(id), cancellationToken);
        if (user is null)
            return Result.Fail(ValidationMessage.UserNotFound);

        var removeRoleResult = await _signInManager.UserManager.RemoveFromRoleAsync(user, IdentityConsts.ROLE_USER);
        if (removeRoleResult.Succeeded is false)
            return Result.Fail(removeRoleResult.Errors.Select(_ => _.Description).FirstOrDefault());

        var addToRoleResult = await _signInManager.UserManager.AddToRoleAsync(user, IdentityConsts.ROLE_ADMIN);
        if (addToRoleResult.Succeeded is false)
            return Result.Fail(addToRoleResult.Errors.Select(_ => _.Description).FirstOrDefault());

        _logger.LogInformation($"User {user.UserName} Successfully Role Upgraded.");

        return Result.Ok();
    }

    public async Task<Result<DetailUserDto>> CurrentUser(CancellationToken cancellationToken)
    {
        var currentUser = await _signInManager.UserManager.GetUserAsync(_signInManager.Context.User);

        string userDisplayRole = await _roleRepository.GetRoleDisplayNameByUserAsync(currentUser.Id, cancellationToken);

        var currentUserRole = _signInManager.Context.User.Claims.Where(_ => _.Type == ClaimTypes.Role).First().Value;

        var user = await _userRepository.GetUser(currentUser.Id, currentUserRole,
            _ => new DetailUserDto(_.Id, _.FullName(), _.PhoneNumber, _.Email,
            _.NationalCode, _.Status,
            _.CreatedDate.ToPersian(), null,
            null,
            null, null)
            { Role = userDisplayRole, IsAdmin = currentUserRole.ToLower() != "user" }, cancellationToken);

        return user;
    }
}