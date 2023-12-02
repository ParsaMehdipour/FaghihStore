using FluentResults;
using MediatR;
using VG.Domain.Enums;

namespace VG.Application.Varieties.Commands.CreateVariety;

public record CreateVarietyCommand(string Title, string ColorCode, string Size, BoxType BoxType, Guid VarietyGroupId) : IRequest<Result>;

