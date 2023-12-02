using MediatR;

using Microsoft.EntityFrameworkCore;

using PM.Domain.ProductImageAgg;

namespace PM.Application.ProductImages.Commands.SetThumbnailProductImage;

public class SetThumbnailProductImageCommandHandler : IRequestHandler<SetThumbnailProductImageCommand, Result<Guid>>
{
    public IProductImageRepository _productImageRepository { get; }

    public SetThumbnailProductImageCommandHandler(IProductImageRepository productImageRepository)
    {
        _productImageRepository = productImageRepository;
    }

    public async Task<Result<Guid>> Handle(SetThumbnailProductImageCommand request, CancellationToken cancellationToken)
    {
        var productImages = await _productImageRepository.Get(pi => pi.ProductId.Equals(request.ProductId) && (pi.IsThumbnail || pi.Id.Equals(request.Id))).ToListAsync(cancellationToken);

        if (productImages.Any(_ => _.Id.Equals(request.Id)) is false)
            ArgumentNullException.ThrowIfNull(productImages);

        if (productImages.Count != 2)
            throw new ArgumentOutOfRangeException(nameof(productImages));

        var productImageToThumbnail = productImages.FirstOrDefault(pi => pi.Id.Equals(request.Id));

        var thumbnailProductImage = productImages.FirstOrDefault(pi => pi.IsThumbnail);

        productImageToThumbnail.SetThumbnail(true);
        productImageToThumbnail.SetTitle(request.Title);
        productImageToThumbnail.SetAlt(request.Alt);
        productImageToThumbnail.SetModifiedDate(DateTime.Now);

        thumbnailProductImage.SetThumbnail(false);
        thumbnailProductImage.SetTitle(string.Empty);
        thumbnailProductImage.SetAlt(string.Empty);
        thumbnailProductImage.SetModifiedDate(DateTime.Now);

        _productImageRepository.UpdateRange(new()
        {
            productImageToThumbnail,
            thumbnailProductImage
        });

        await _productImageRepository.SaveAsync(cancellationToken);

        return Result.Ok();
    }
}