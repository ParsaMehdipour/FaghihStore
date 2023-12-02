using MediatR;
using Microsoft.EntityFrameworkCore;
using PM.Domain.ProductVarietyAggregate;

namespace PM.Application.ProductVarieties.Queries.GetProductsWithVarieties;

public class GetProductsWithVarietiesQueryHandler : IRequestHandler<GetProductsWithVarietiesQuery, Result<List<GetProductsWithVarietiesDto>>>
{
    protected IProductVarietyRepository _productVarietyRepository { get; }

    public GetProductsWithVarietiesQueryHandler(IProductVarietyRepository productVarietyRepository)
    {
        _productVarietyRepository = productVarietyRepository;
    }

    public async Task<Result<List<GetProductsWithVarietiesDto>>> Handle(GetProductsWithVarietiesQuery request, CancellationToken cancellationToken)
    {
        List<GetProductsWithVarietiesDto> result = new();

        var productVarieties = await _productVarietyRepository.Get(_ => request.ProductVarietiesDictionary.Values.Contains(_.Id)).Include(_ => _.Product).ToListAsync(cancellationToken);

        foreach (var dictionary in request.ProductVarietiesDictionary)
        {
            ProductVariety productVariety = productVarieties.FirstOrDefault(_ => _.Id == dictionary.Value)!;

            GetProductsWithVarietiesDto resultDto = new(dictionary.Key, productVariety.Product.TitlePersian, "VarietyTitle");

            result.Add(resultDto);
        }

        return Result.Ok(result);
    }
}
