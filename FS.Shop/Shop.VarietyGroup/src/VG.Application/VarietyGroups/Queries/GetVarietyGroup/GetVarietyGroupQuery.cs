using FluentResults;
using MediatR;
using VG.Application.VarietyGroups.Commands.EditVarietyGroup;
using VG.Domain.Models;

namespace VG.Application.VarietyGroups.Queries.GetVarietyGroup;

public record GetVarietyGroupQuery(Guid Id) : IRequest<Result<EditVarietyGroupCommand>>
{
    internal EditVarietyGroupCommand ToCommand(VarietyGroup varietyGroup) =>
        new EditVarietyGroupCommand(varietyGroup.Id,
            varietyGroup.Title);
}



