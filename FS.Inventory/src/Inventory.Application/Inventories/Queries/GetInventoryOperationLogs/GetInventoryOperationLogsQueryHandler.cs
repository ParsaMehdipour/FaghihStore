using DNTPersianUtils.Core;
using FluentResults;
using Inventory.Application.Criteria;
using Inventory.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SH.Infrastructure.Criteria;
using SH.Infrastructure.Criteria.Pagination;

namespace Inventory.Application.Inventories.Queries.GetInventoryOperationLogs;

public class GetInventoryOperationLogsQueryHandler : IRequestHandler<GetInventoryOperationLogsQuery, Result<ResponseModel<IEnumerable<GetInventoryOperationLogsDto>, InventoryOperationQueryStringParameters>>>
{
    protected IInventoryRepository _inventoryRepository { get; }

    public GetInventoryOperationLogsQueryHandler(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task<Result<ResponseModel<IEnumerable<GetInventoryOperationLogsDto>, InventoryOperationQueryStringParameters>>> Handle(GetInventoryOperationLogsQuery request, CancellationToken cancellationToken)
    {
        var operations = await _inventoryRepository.Get(_ => _.Id == request.InventoryId).Include(_ => _.Operations).Select(_ => _.Operations).FirstOrDefaultAsync(cancellationToken);

        ArgumentNullException.ThrowIfNull(operations, nameof(operations));

        var operationsResult = operations.Select(_ => new GetInventoryOperationLogsDto(
            _.OperationType,
            _.Count,
            "Admin",
            _.OperationDate.ToPersianDateTimeString("yy/MM/dd HH:MM:ss"),
            _.CurrentCount,
            _.Description,
            _.OrderId == Guid.Empty ? "سیستم" : _.OrderId.ToString()
        )).ToList();

        int count = operationsResult.Count();
        var pager = new Pager(count, request.Parameters.PageNumber);

        ResponseModel<IEnumerable<GetInventoryOperationLogsDto>, InventoryOperationQueryStringParameters> result = new()
        {
            Model = operationsResult.AsReadOnly(),
            Parameters = request.Parameters,
            Pager = pager
        };

        return Result.Ok(result);
    }
}
