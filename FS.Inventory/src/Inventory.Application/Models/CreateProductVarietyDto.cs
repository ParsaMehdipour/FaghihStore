namespace Inventory.Application.Models;

public record CreateProductVarietyDto(Guid ProductId, Guid VarietyId, Guid InventoryId);