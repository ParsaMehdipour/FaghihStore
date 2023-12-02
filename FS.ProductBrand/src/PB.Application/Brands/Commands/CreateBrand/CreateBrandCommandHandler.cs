using MediatR;

using PB.Domain.Models;

namespace PB.Application.Brands.Commands.CreateBrand;

public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, Result<Guid>>
{
    protected IBrandRepository _brandRepository { get; }
    protected BrandFactory _brandFactory { get; }

    public CreateBrandCommandHandler(IBrandRepository brandRepository, BrandFactory brandFactory)
    {
        _brandRepository = brandRepository;
        _brandFactory = brandFactory;
    }

    public async Task<Result<Guid>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        Brand brand = _brandFactory.Create(request.Title, request.OrderNumber, request.Slug, request.Status);

        await _brandRepository.AddAsync(brand, cancellationToken);
        await _brandRepository.SaveAsync(cancellationToken);

        return Result.Ok(brand.Id);

    }
}
