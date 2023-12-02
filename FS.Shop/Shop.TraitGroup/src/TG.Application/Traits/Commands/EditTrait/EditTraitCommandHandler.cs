using MediatR;

using TG.Domain.Models;

namespace TG.Application.Traits.Commands.EditTrait;

public class EditTraitCommandHandler : IRequestHandler<EditTraitCommand, Result<Guid>>
{
    protected ITraitRepository _traitRepository { get; }

    public EditTraitCommandHandler(ITraitRepository traitRepository)
    {
        _traitRepository = traitRepository;
    }

    public async Task<Result<Guid>> Handle(EditTraitCommand request, CancellationToken cancellationToken)
    {
        Trait trait = await _traitRepository.GetByIdAsync(cancellationToken, request.Id);

        ArgumentNullException.ThrowIfNull(trait, nameof(trait));

        trait.SetTitle(request.Title);
        trait.SetOrderNumber(request.OrderNumber);
        trait.SetHasFilterAbility(request.HasFilterAbility);
        trait.SetTraitGroupId(request.TraitGroupId);
        trait.SetCategoryId(request.CategoryId);
        trait.SetModifiedDate(DateTime.Now);

        await _traitRepository.SaveAsync(cancellationToken);

        return Result.Ok();
    }
}