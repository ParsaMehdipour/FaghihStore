using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Role.Domain.Models;

using SH.Infrastructure.Criteria;
using SH.Infrastructure.Criteria.Pagination;
using SH.Infrastructure.Extensions;

namespace Role.Application.Roles.Queries.GetRoles;

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, Result<ResponseModel<IEnumerable<GetRoleDto>>>>
{
    protected RoleManager<ApplicationRole> _roleManager { get; }

    public GetRolesQueryHandler(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<Result<ResponseModel<IEnumerable<GetRoleDto>>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = _roleManager.Roles.IgnoreQueryFilters().Where(_ => _.IsDeleted == request.Parameters.IsDeleted);

        var count = await _roleManager.Roles.CountAsync(cancellationToken: cancellationToken);
        var pager = new Pager(count, request.Parameters.PageNumber);

        if (!string.IsNullOrWhiteSpace(request.Parameters.Search))
            roles = roles.Where(_ => _.Name.Contains(request.Parameters.Search) ||
                                     _.DisplayName.Contains(request.Parameters.Search));

        var result = await roles.Paginate(pager).Select(_ => new GetRoleDto(
                _.Id,
                _.Name,
                _.DisplayName,
                _.IsDeleted,
                _.CreatedDate.ToPersian()))
            .ToListAsync(cancellationToken);

        ResponseModel<IEnumerable<GetRoleDto>> responseModel = new ResponseModel<IEnumerable<GetRoleDto>>()
        {
            Model = result.AsReadOnly(),
            Pager = pager,
            Parameters = request.Parameters
        };

        return Result.Ok(responseModel);
    }
}