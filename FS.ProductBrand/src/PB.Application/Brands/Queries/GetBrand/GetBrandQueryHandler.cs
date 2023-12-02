using MediatR;

using PB.Application.Brands.Commands.EditBrand;

namespace PB.Application.Brands.Queries.GetBrand;

public class GetBrandQueryHandler : IRequestHandler<GetBrandQuery, Result<EditBrandCommand>>
{
    protected IBrandRepository _brandRepository { get; }

    public GetBrandQueryHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<Result<EditBrandCommand>> Handle(GetBrandQuery request, CancellationToken cancellationToken)
    {
        var brand = await _brandRepository.GetWithoutQueryFilterAsync(_ => _.Id == request.Id, cancellationToken);

        ArgumentNullException.ThrowIfNull(brand, nameof(brand));

        var brandDto = new EditBrandCommand(Id: brand.Id, OrderNumber: brand.OrderNumber, Slug: brand.Slug, Title: brand.Title, Status: brand.Status);

        return Result.Ok(brandDto);
    }
}
