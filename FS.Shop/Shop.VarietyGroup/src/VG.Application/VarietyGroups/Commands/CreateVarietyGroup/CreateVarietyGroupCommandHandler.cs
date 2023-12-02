using FluentResults;
using MediatR;
using VG.Domain.Models;
using VG.Domain.Repositories;

namespace VG.Application.VarietyGroups.Commands.CreateVarietyGroup;

public class CreateVarietyGroupCommandHandler : IRequestHandler<CreateVarietyGroupCommand, Result<Guid>>
{

    protected IVarietyGroupRepository _repository { get; }
    protected VarietyGroupFactory _factory { get; }

    public CreateVarietyGroupCommandHandler(IVarietyGroupRepository repository, VarietyGroupFactory factory)
    {
        _repository = repository;
        _factory = factory;
    }

    public async Task<Result<Guid>> Handle(CreateVarietyGroupCommand request, CancellationToken cancellationToken)
    {
        VarietyGroup varietyGroup = _factory.Create(request.Title);

        await _repository.AddAsync(varietyGroup, cancellationToken);
        await _repository.SaveAsync(cancellationToken);

        return Result.Ok(varietyGroup.Id);
    }
}
