using MediatR;

using Role.Application.Criteria;

using SH.Infrastructure.Criteria;

namespace Role.Application.Roles.Queries.GetRoles;

public record GetRolesQuery(RoleQueryStringParameters Parameters) : IRequest<Result<ResponseModel<IEnumerable<GetRoleDto>>>>;

public record GetRoleDto(Guid Id, string Name, string DisplayName, bool IsDeleted, string CreateDate);
