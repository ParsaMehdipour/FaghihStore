using FluentResults;
using MediatR;

namespace VG.Application.VarietyGroups.Commands.EditVarietyGroup;

public record EditVarietyGroupCommand(Guid Id, string Title) : IRequest<Result>;

