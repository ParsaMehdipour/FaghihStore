using MediatR;

using Microsoft.EntityFrameworkCore;

using PM.Domain.ProductImageAgg;

using SH.Application.Interfaces;

namespace PM.Application.ProductImages.Commands.DeleteProductImage;

public class DeleteProductImageCommandHandler : IRequestHandler<DeleteProductImageCommand, Result>
{
    protected IProductImageRepository _productImageRepository { get; }
    protected IFileUploaderService _fileUploaderService { get; }

    public DeleteProductImageCommandHandler(IProductImageRepository productImageRepository,
        IFileUploaderService fileUploaderService)
    {
        _productImageRepository = productImageRepository;
        _fileUploaderService = fileUploaderService;
    }

    public async Task<Result> Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
    {
        ProductImage productImage = await _productImageRepository.Get(_ => _.Id == request.Id && _.ProductId.Equals(request.ProductId)).FirstOrDefaultAsync(cancellationToken);

        ArgumentNullException.ThrowIfNull(productImage, paramName: nameof(productImage));

        _productImageRepository.Delete(productImage);

        _fileUploaderService.DeleteFile(productImage.GetFileNamePath());

        await _productImageRepository.SaveAsync(cancellationToken);

        return Result.Ok();
    }
}