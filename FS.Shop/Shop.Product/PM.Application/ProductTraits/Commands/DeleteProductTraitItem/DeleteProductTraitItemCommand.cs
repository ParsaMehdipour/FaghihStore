using MediatR;

namespace VG.Application.VarietyGroups.Commands.DeleteVarietyGroup;

public record DeleteProductTraitItemCommand(Guid Id, bool IsRestored) : IRequest<Result>;