using FluentResults;
using MediatR;
using VG.Domain.Enums;

namespace VG.Application.Varieties.Commands.EditVariety;

public record EditVarietyCommand(Guid Id, string Title, string ColorCode, string Size, BoxType BoxType, Guid VarietyGroupId) : IRequest<Result>;
