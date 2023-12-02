using _0_Framework.Application;

using MediatR;

using PM.Domain.ProductAgg;

using SH.Application.Interfaces;

namespace PM.Application.Products.Commands.EditProduct;

public class EditProductCommandHandler : IRequestHandler<EditProductCommand, Result<Guid>>
{
    public const string PRODUCT_UPLOAD_PATH = "Product";

    protected IProductRepository _productRepository { get; }
    protected IFileUploaderService _fileUploaderService { get; }
    public EditProductCommandHandler(IProductRepository productRepository,
        IFileUploaderService fileUploaderService)
    {
        _productRepository = productRepository;
        _fileUploaderService = fileUploaderService;
    }

    public async Task<Result<Guid>> Handle(EditProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetIncludeThumbnailImageAsync(request.Id, cancellationToken);

        if (product is null)
            return Result.Fail("Product has empty");

        var uploadPath = $"{PRODUCT_UPLOAD_PATH}\\{request.CategoryId}";

        product.SetTitlePersian(request.TitlePersian);
        product.SetTitleEnglish(request.TitleEnglish);
        product.SetSlug(request.Slug.Slugify());
        product.SetMetaDescription(request.MetaDescription);
        product.SetCategory(request.CategoryId);
        product.SetBrandId(request.BrandId);
        product.SetPublishedDate(request.PublishedDate);
        product.SetWarrantyDescription(request.WarrantyDescription);

        var productImage = product.Images.FirstOrDefault();

        productImage.SetTitle(request.Image.Title);
        productImage.SetAlt(request.Image.Alt);

        bool productImageFileHasChanged = false;
        string oldImageFullPath = productImage.GetFileNamePath();
        string newImageFullPath = string.Empty;

        if (request.Image.File is not null)
        {
            productImageFileHasChanged = true;

            newImageFullPath = _fileUploaderService.CreateFileName(request.Image.File.FileName, prefixFileName: string.Empty, uploadPath);

            productImage = request.Image.ToModel(productImage, _fileUploaderService.ReplaceFileNameAfterCreated(newImageFullPath));

            product.Images[0] = productImage;
        }

        _productRepository.Update(product);
        await _productRepository.SaveAsync(cancellationToken);

        if (productImageFileHasChanged)
        {
            await _fileUploaderService.SaveFile(request.Image.File, newImageFullPath, FileMode.Create, cancellationToken);

            _fileUploaderService.DeleteFile(oldImageFullPath);
        }

        return Result.Ok(product.Id);
    }
}