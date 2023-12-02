using CD.Domain.Models;
using CD.Domain.Repositories;
using FluentResults;
using MediatR;

namespace CD.Application.CountryDivisions.Commands.EditCountryDivision;

public class EditCountryDivisionCommandHandler : IRequestHandler<EditCountryDivisionCommand, Result>
{
    protected ICountryDivisionRepository _countryDivisionRepository { get; }

    public EditCountryDivisionCommandHandler(ICountryDivisionRepository countryDivisionRepository)
    {
        _countryDivisionRepository = countryDivisionRepository;
    }

    public async Task<Result> Handle(EditCountryDivisionCommand request, CancellationToken cancellationToken)
    {
        CountryDivision countryDivision = await _countryDivisionRepository.GetByIdAsync(cancellationToken, request.Id);

        ArgumentNullException.ThrowIfNull(countryDivision);

        countryDivision.SetName(request.Name);
        countryDivision.SetParent(request.ParentId);

        //_countryDivisionRepository.Update(countryDivision);
        await _countryDivisionRepository.SaveAsync(cancellationToken);

        return Result.Ok();
    }
}
