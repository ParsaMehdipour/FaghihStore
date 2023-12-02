using MediatR;
using Microsoft.EntityFrameworkCore;
using PM.Domain.ProductDescriptionAgg;
using SH.Infrastructure.Criteria;

namespace PM.Application.ProductDescriptions.Queries.GetProductDescriptions;

public class GetProductDescriptionsQueryHandler : IRequestHandler<GetProductDescriptionsQuery, Result<ResponseModel<IEnumerable<GetProductDescriptionDto>>>>
{

    protected IProductDescriptionRepository _productDescriptionRepository { get; }

    public GetProductDescriptionsQueryHandler(IProductDescriptionRepository productDescriptionRepository)
    {
        _productDescriptionRepository = productDescriptionRepository;
    }

    public async Task<Result<ResponseModel<IEnumerable<GetProductDescriptionDto>>>> Handle(GetProductDescriptionsQuery request, CancellationToken cancellationToken)
    {
        var productDescriptions = _productDescriptionRepository.Get();

        productDescriptions = productDescriptions.Where(_ => _.ProductId == request.ProductId);

        var result = await productDescriptions.Select(_ => new GetProductDescriptionDto(
                _.Id,
                _.Title,
                (_.Description.Length > 15) ? _.Description.Substring(0, Math.Min(_.Description.Length, 15)) + "..." : _.Description))
            .ToListAsync(cancellationToken);


        ResponseModel<IEnumerable<GetProductDescriptionDto>> responseModel = new()
        {
            Model = result.AsReadOnly()
        };

        return Result.Ok(responseModel);

    }
}
