using FluentResults;
using MediatR;

namespace VG.Application.VarietyGroups.Commands.CreateVarietyGroup;

public record CreateVarietyGroupCommand(string Title) : IRequest<Result<Guid>>;

