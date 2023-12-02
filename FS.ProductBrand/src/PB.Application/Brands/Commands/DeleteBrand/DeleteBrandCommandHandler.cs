using MediatR;

using PB.Domain.Models;

namespace PB.Application.Brands.Commands.DeleteBrand;

public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, Result>
{
    protected IBrandRepository _brandRepository { get; }

    public DeleteBrandCommandHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<Result> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        Brand brand = await _brandRepository.GetWithoutQueryFilterAsync(_ => _.Id == request.Id, cancellationToken);

        ArgumentNullException.ThrowIfNull(brand, nameof(brand));

        if (request.IsRestored == false) brand.DeleteBrand();
        else brand.RestoreBrand();

        _brandRepository.Update(brand);
        await _brandRepository.SaveAsync(cancellationToken);

        return Result.Ok();
    }
}
