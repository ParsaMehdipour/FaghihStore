using CD.Domain.Models;
using CD.Domain.Repositories;
using FluentResults;
using MediatR;

namespace CD.Application.CountryDivisions.Commands.CreateCountryDivision;

public class CreateCountryDivisionCommandHandler : IRequestHandler<CreateCountryDivisionCommand, Result<Guid>>
{
    protected ICountryDivisionRepository _repository { get; }
    protected CountryDivisionFactory _countryDivisionFactory { get; }
    public CreateCountryDivisionCommandHandler(ICountryDivisionRepository repository, CountryDivisionFactory countryDivisionFactory)
    {
        _repository = repository;
        _countryDivisionFactory = countryDivisionFactory;
    }

    public async Task<Result<Guid>> Handle(CreateCountryDivisionCommand request, CancellationToken cancellationToken)
    {
        CountryDivision countryDivision = _countryDivisionFactory.Create(request.Name);

        if (request.ParentId.Equals(Guid.Empty) is false)
            countryDivision.SetParent(request.ParentId);

        await _repository.AddAsync(countryDivision, cancellationToken);
        await _repository.SaveAsync(cancellationToken);

        return Result.Ok(countryDivision.Id);
    }
}
