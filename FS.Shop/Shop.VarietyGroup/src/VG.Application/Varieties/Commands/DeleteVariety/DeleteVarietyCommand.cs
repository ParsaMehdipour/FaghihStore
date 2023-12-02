using FluentResults;
using MediatR;

namespace VG.Application.Varieties.Commands.DeleteVariety;

public record DeleteVarietyCommand(Guid Id, bool IsRestored) : IRequest<Result>;

