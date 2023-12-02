using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using PM.Domain.ProductAgg;
using PM.Domain.ProductImageAgg;

using SH.Application.Interfaces;

namespace PM.Application.ProductImages.Commands.CreateProductImage;

public class CreateProductImageCommandHandler : IRequestHandler<CreateProductImageCommand, Result<Guid>>
{
    public const string PRODUCT_UPLOAD_PATH = "Product";

    protected IFileUploaderService _fileUploaderService { get; }
    protected IProductImageRepository _productImageRepository { get; }
    protected IProductRepository _productRepository { get; }
    protected ProductFactory _productFactory { get; }

    public CreateProductImageCommandHandler(IFileUploaderService fileUploaderService,
        IProductImageRepository productImageRepository,
        IProductRepository productRepository,
        ProductFactory productFactory)
    {
        _fileUploaderService = fileUploaderService;
        _productImageRepository = productImageRepository;
        _productRepository = productRepository;
        _productFactory = productFactory;
    }

    public async Task<Result<Guid>> Handle(CreateProductImageCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.Get(_ => _.Id.Equals(request.ProductId))
            .Include(_ => _.Images)
            .FirstOrDefaultAsync(cancellationToken);

        var uploadPath = $"{PRODUCT_UPLOAD_PATH}\\{product.CategoryId}";

        var imagesFullPath = _fileUploaderService.CreateFileName(request.Files, prefixFileName: string.Empty, uploadPath);

        //replace all of images name to right way
        List<(IFormFile File, string NewFileName)> replacedFileNames = new(imagesFullPath.Count);
        imagesFullPath.ForEach(_ =>
        {
            replacedFileNames.Add((_.File, _fileUploaderService.ReplaceFileNameAfterCreated(_.NewFileName)));
        });

        product.Images.AddRange(request.ToModel(_productFactory, replacedFileNames));

        _productRepository.Update(product);
        await _productRepository.SaveAsync(cancellationToken);

        await _fileUploaderService.SaveFile(imagesFullPath, FileMode.Create, cancellationToken);

        return Result.Ok(product.Id);
    }
}