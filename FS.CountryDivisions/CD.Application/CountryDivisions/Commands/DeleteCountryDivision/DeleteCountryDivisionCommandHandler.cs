using CD.Domain.Models;
using CD.Domain.Repositories;
using FluentResults;
using MediatR;

namespace CD.Application.CountryDivisions.Commands.DeleteCountryDivision;

public class DeleteCountryDivisionCommandHandler : IRequestHandler<DeleteCountryDivisionCommand, Result>
{

    protected ICountryDivisionRepository _countryDivisionRepository { get; }

    public DeleteCountryDivisionCommandHandler(ICountryDivisionRepository countryDivisionRepository)
    {
        _countryDivisionRepository = countryDivisionRepository;
    }

    public async Task<Result> Handle(DeleteCountryDivisionCommand request, CancellationToken cancellationToken)
    {
        CountryDivision countryDivision = await _countryDivisionRepository.GetWithoutQueryFilterAsync(_ => _.Id == request.Id, cancellationToken);

        ArgumentNullException.ThrowIfNull(countryDivision, nameof(countryDivision));

        if (countryDivision.ParentId == null)
            if (await _countryDivisionRepository.IsExistsAsync(_ => _.ParentId == countryDivision.Id, cancellationToken))
                return Result.Fail("Can't Delete County"); //TODO Use Toastify Notification

        if (request.IsRestored)
            countryDivision.RestoreItem();
        else
            countryDivision.DeleteItem();

        await _countryDivisionRepository.SaveAsync(cancellationToken);

        return Result.Ok();
    }
}
