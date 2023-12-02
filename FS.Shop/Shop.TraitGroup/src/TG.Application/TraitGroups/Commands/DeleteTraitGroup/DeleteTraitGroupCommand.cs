using MediatR;

namespace VG.Application.VarietyGroups.Commands.DeleteVarietyGroup;

public record DeleteTraitGroupCommand(Guid Id, bool IsRestored) : IRequest<Result>;