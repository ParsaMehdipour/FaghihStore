using FluentResults;
using Inventory.Application.Criteria;
using Inventory.Domain.Enums;
using MediatR;
using SH.Infrastructure.Criteria;

namespace Inventory.Application.Inventories.Queries.GetInventoryOperationLogs;

public record GetInventoryOperationLogsQuery(Guid InventoryId, InventoryOperationQueryStringParameters Parameters) : IRequest<Result<ResponseModel<IEnumerable<GetInventoryOperationLogsDto>, InventoryOperationQueryStringParameters>>>;

public record GetInventoryOperationLogsDto(InventoryOperationType OperationType, long Count, string OperatorFullName, string OperationDate, long CurrentCount, string Description, string OrderId);
