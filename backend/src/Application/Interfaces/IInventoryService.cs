using JAS.ECommerce.Application.DTOs.Common;
using JAS.ECommerce.Application.DTOs.Inventory;

namespace JAS.ECommerce.Application.Interfaces;

public interface IInventoryService
{
    Task<InventoryDto?> GetInventoryAsync(int productId, int? variantId = null);
    Task<bool> ReserveStockAsync(int productId, int quantity, int? variantId = null);
    Task<bool> ReleaseReservedStockAsync(int productId, int quantity, int? variantId = null);
    Task<bool> DeductStockAsync(int productId, int quantity, string reference, int? variantId = null);
    Task<bool> AddStockAsync(int productId, int quantity, string reference, int? variantId = null);
    Task<bool> AdjustStockAsync(int productId, int quantity, string notes, int? variantId = null);
    Task<IEnumerable<InventoryTransactionDto>> GetTransactionHistoryAsync(int inventoryId);
    Task<IEnumerable<InventoryDto>> GetLowStockItemsAsync();
}
