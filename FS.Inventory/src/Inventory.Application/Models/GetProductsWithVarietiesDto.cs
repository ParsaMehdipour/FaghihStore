namespace Inventory.Application.Models;

public record GetProductsWithVarietiesDto(Guid InventoryId, string ProductTitle, string VarietyTitle);
