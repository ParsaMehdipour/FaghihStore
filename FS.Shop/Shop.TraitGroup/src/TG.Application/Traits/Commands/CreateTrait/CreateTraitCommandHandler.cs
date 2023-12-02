using MediatR;

using TG.Domain.Models;

namespace TG.Application.Traits.Commands.CreateTrait;

public class CreateTraitCommandHandler : IRequestHandler<CreateTraitCommand, Result<Guid>>
{
    public ITraitRepository _traitRepository { get; }
    public TraitGroupFactory _factory { get; }

    public CreateTraitCommandHandler(ITraitRepository traitRepository,
        TraitGroupFactory factory)
    {
        _traitRepository = traitRepository;
        _factory = factory;
    }

    public async Task<Result<Guid>> Handle(CreateTraitCommand request, CancellationToken cancellationToken)
    {
        var trait = _factory.CreateTrait(request.Title, request.OrderNumber,
            request.TraitGroupId, request.CategoryId,
            request.HasFilterAbility);

        await _traitRepository.AddAsync(trait, cancellationToken);
        await _traitRepository.SaveAsync(cancellationToken);

        return Result.Ok(trait.Id);
    }
}