using FluentResults;
using MediatR;
using VG.Application.Varieties.Commands.EditVariety;
using VG.Domain.Models;

namespace VG.Application.Varieties.Queries.GetVariety;

public record GetVarietyQuery(Guid Id) : IRequest<Result<EditVarietyCommand>>
{
    internal EditVarietyCommand ToCommand(Variety variety) =>
        new EditVarietyCommand(
            variety.Id,
            variety.Title,
            variety.ColorCode,
            variety.Size,
            variety.BoxType,
            variety.VarietyGroupId);
}

