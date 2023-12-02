using _0_Framework.Application;

using MediatR;

using PM.Domain.ProductAgg;

using SH.Application.Interfaces;

namespace PM.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Guid>>
{
    public const string PRODUCT_UPLOAD_PATH = "Product";

    protected ProductFactory _productFactory { get; }
    protected IProductRepository _productRepository { get; }
    protected IFileUploaderService _fileUploaderService { get; }
    public CreateProductCommandHandler(ProductFactory productFactory,
        IProductRepository productRepository,
        IFileUploaderService fileUploaderService)
    {
        _productFactory = productFactory;
        _productRepository = productRepository;
        _fileUploaderService = fileUploaderService;
    }

    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var uploadPath = $"{PRODUCT_UPLOAD_PATH}\\{request.CategoryId}";

        var imageFullPath = _fileUploaderService.CreateFileName(request.Image.File.FileName, prefixFileName: string.Empty, uploadPath);

        var product = _productFactory.Create(request.TitlePersian,
            request.TitleEnglish,
            request.CategoryId,
            request.BrandId,
            request.Slug.Slugify(),
            request.MetaDescription,
            request.WarrantyDescription,
            request.PublishedDate,
            request.Image.ToModel(_productFactory, _fileUploaderService.ReplaceFileNameAfterCreated(imageFullPath)));

        await _productRepository.AddAsync(product, cancellationToken);
        await _productRepository.SaveAsync(cancellationToken);

        await _fileUploaderService.SaveFile(request.Image.File, imageFullPath, FileMode.Create, cancellationToken);

        return Result.Ok(product.Id);
    }
}