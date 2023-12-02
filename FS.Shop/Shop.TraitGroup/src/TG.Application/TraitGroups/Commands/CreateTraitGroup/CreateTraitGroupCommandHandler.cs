using MediatR;

using TG.Domain.Models;

namespace TG.Application.TraitGroups.Commands.CreateTraitGroup;

public class CreateTraitGroupCommandHandler : IRequestHandler<CreateTraitGroupCommand, Result<Guid>>
{
    protected ITraitGroupRepository _traitGroupRepository { get; set; }
    protected TraitGroupFactory _factory { get; set; }

    public CreateTraitGroupCommandHandler(ITraitGroupRepository traitGroupRepository,
        TraitGroupFactory factory)
    {
        _traitGroupRepository = traitGroupRepository;
        _factory = factory;
    }

    public async Task<Result<Guid>> Handle(CreateTraitGroupCommand request, CancellationToken cancellationToken)
    {
        var traitGroup = _factory.Create(request.Title, request.OrderNumber);

        await _traitGroupRepository.AddAsync(traitGroup, cancellationToken);
        await _traitGroupRepository.SaveAsync(cancellationToken);

        return Result.Ok(traitGroup.Id);
    }
}