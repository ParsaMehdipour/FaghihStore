using FluentResults;
using MediatR;

namespace VG.Application.VarietyGroups.Queries.GetVarietyGroupsForVariety;

public record GetVarietyGroupsForVarietyQuery : IRequest<Result<IEnumerable<GetVarietyGroupForVarietyDto>>>;

public record GetVarietyGroupForVarietyDto(Guid Id, string Title);
