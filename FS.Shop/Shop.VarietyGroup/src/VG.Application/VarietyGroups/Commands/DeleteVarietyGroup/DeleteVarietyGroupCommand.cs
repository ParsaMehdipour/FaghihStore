using FluentResults;
using MediatR;

namespace VG.Application.VarietyGroups.Commands.DeleteVarietyGroup;

public record DeleteVarietyGroupCommand(Guid Id, bool IsRestored) : IRequest<Result>;

