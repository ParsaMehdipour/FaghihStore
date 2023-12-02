using MediatR;

using PB.Domain.Models;

namespace PB.Application.Brands.Commands.EditBrand;

public class EditBrandCommandHandler : IRequestHandler<EditBrandCommand, Result>
{
    protected IBrandRepository _brandRepository { get; }

    public EditBrandCommandHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<Result> Handle(EditBrandCommand request, CancellationToken cancellationToken)
    {
        Brand brand = await _brandRepository.GetByIdAsync(cancellationToken, request.Id);

        ArgumentNullException.ThrowIfNull(brand);

        brand.SetTitle(request.Title);
        brand.SetSlug(request.Slug);
        brand.SetOrderNumber(request.OrderNumber);
        brand.SetStatus(request.Status);

        _brandRepository.Update(brand);

        await _brandRepository.SaveAsync(cancellationToken);

        return Result.Ok();
    }
}
