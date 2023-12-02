
using Microsoft.EntityFrameworkCore;

using SH.Infrastructure.EfCore.Repositories;

using System.Data;
using System.Linq.Expressions;

using User.Domain.Models;
using User.Domain.Repositories;

namespace Identity.Infrastructure.Persistence.Repositories;

public class UserRepository : EfRepository<ApplicationUser>, IUserRepository
{
    protected IdentityContext _identityContext { get; }

    public UserRepository(IdentityContext identityContext) : base(identityContext)
    {
        _identityContext = identityContext;
    }

    public IQueryable<ApplicationUser> GetUsersByRole(string role)
    {
        var roleId = _identityContext.Roles.AsNoTracking().Where(_ => _.Name.Equals(role)).Select(_ => _.Id)
            .SingleOrDefault();

        return from userRole in _identityContext.UserRoles
               join user in _identityContext.Users on userRole.UserId equals user.Id
               where userRole.RoleId.Equals(roleId)
               select user;
    }

    public async Task<TResult> GetUser<TResult>(Guid id, string role, Expression<Func<ApplicationUser, TResult>> selector, CancellationToken cancellationToken)
    {
        return await this.GetUsersByRole(role).AsNoTracking().Where(_ => _.Id.Equals(id)).Select(selector).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> PhoneNumberBeUnique(Guid id, string phoneNumber, bool isForModify, CancellationToken cancellationToken)
    {
        bool isExists;

        if (isForModify is false)
            isExists = await this.IsExistsAsync(_ => _.PhoneNumber.Equals(phoneNumber), cancellationToken);
        else
            isExists = await this.IsExistsAsync(_ => !_.Id.Equals(id) && _.PhoneNumber.Equals(phoneNumber),
                    cancellationToken);

        return isExists;
    }

    public async Task<bool> EmailBeUnique(Guid id, string email, bool isForModify, CancellationToken cancellationToken)
    {
        bool isExists = false;

        //we add this statement because Email is not required to set value.
        if (!string.IsNullOrWhiteSpace(email))
            if (isForModify is false)
                isExists = await this.IsExistsAsync(_ => _.Email.Equals(email), cancellationToken);
            else
                isExists = await this.IsExistsAsync(_ => !_.Id.Equals(id) && _.Email.Equals(email),
                    cancellationToken);

        return isExists;
    }
}