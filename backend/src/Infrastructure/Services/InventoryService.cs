using JAS.ECommerce.Application.DTOs.Inventory;
using JAS.ECommerce.Application.Interfaces;
using JAS.ECommerce.Domain.Entities;
using JAS.ECommerce.Domain.Enums;
using JAS.ECommerce.Domain.Interfaces;

namespace JAS.ECommerce.Infrastructure.Services;

public class InventoryService : IInventoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<InventoryService> _logger;

    public InventoryService(IUnitOfWork unitOfWork, ILogger<InventoryService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<InventoryDto?> GetInventoryAsync(int productId, int? variantId = null)
    {
        try
        {
            var inventories = await _unitOfWork.InventoryRepository.GetAllAsync();
            var inventory = inventories.FirstOrDefault(i =>
                i.ProductId == productId &&
                (variantId == null ? i.ProductVariantId == null : i.ProductVariantId == variantId));

            if (inventory == null)
            {
                _logger.LogWarning($"Inventory not found for product {productId}");
                return null;
            }

            return MapToDto(inventory);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting inventory");
            throw;
        }
    }

    public async Task<bool> ReserveStockAsync(int productId, int quantity, int? variantId = null)
    {
        try
        {
            var inventories = await _unitOfWork.InventoryRepository.GetAllAsync();
            var inventory = inventories.FirstOrDefault(i =>
                i.ProductId == productId &&
                (variantId == null ? i.ProductVariantId == null : i.ProductVariantId == variantId));

            if (inventory == null)
            {
                throw new KeyNotFoundException("Inventory not found");
            }

            var availableQuantity = inventory.Quantity - inventory.ReservedQuantity;
            if (availableQuantity < quantity)
            {
                _logger.LogWarning($"Insufficient stock for product {productId}");
                return false;
            }

            inventory.ReservedQuantity += quantity;
            _unitOfWork.InventoryRepository.Update(inventory);

            // Log transaction
            var transaction = new InventoryTransaction
            {
                InventoryId = inventory.Id,
                Type = InventoryTransactionType.Reservation,
                Quantity = quantity,
                Reference = $"RESERVE-{DateTime.UtcNow.Ticks}",
                CreatedAt = DateTime.UtcNow
            };
            await _unitOfWork.InventoryTransactionRepository.AddAsync(transaction);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reserving stock");
            throw;
        }
    }

    public async Task<bool> ReleaseReservedStockAsync(int productId, int quantity, int? variantId = null)
    {
        try
        {
            var inventories = await _unitOfWork.InventoryRepository.GetAllAsync();
            var inventory = inventories.FirstOrDefault(i =>
                i.ProductId == productId &&
                (variantId == null ? i.ProductVariantId == null : i.ProductVariantId == variantId));

            if (inventory == null)
            {
                throw new KeyNotFoundException("Inventory not found");
            }

            if (inventory.ReservedQuantity < quantity)
            {
                _logger.LogWarning($"Cannot release more than reserved quantity for product {productId}");
                return false;
            }

            inventory.ReservedQuantity -= quantity;
            _unitOfWork.InventoryRepository.Update(inventory);

            // Log transaction
            var transaction = new InventoryTransaction
            {
                InventoryId = inventory.Id,
                Type = InventoryTransactionType.ReservationCancellation,
                Quantity = quantity,
                Reference = $"RELEASE-{DateTime.UtcNow.Ticks}",
                CreatedAt = DateTime.UtcNow
            };
            await _unitOfWork.InventoryTransactionRepository.AddAsync(transaction);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error releasing reserved stock");
            throw;
        }
    }

    public async Task<bool> DeductStockAsync(int productId, int quantity, string reference, int? variantId = null)
    {
        try
        {
            var inventories = await _unitOfWork.InventoryRepository.GetAllAsync();
            var inventory = inventories.FirstOrDefault(i =>
                i.ProductId == productId &&
                (variantId == null ? i.ProductVariantId == null : i.ProductVariantId == variantId));

            if (inventory == null)
            {
                throw new KeyNotFoundException("Inventory not found");
            }

            if (inventory.Quantity < quantity)
            {
                _logger.LogWarning($"Insufficient stock for product {productId}");
                return false;
            }

            inventory.Quantity -= quantity;
            _unitOfWork.InventoryRepository.Update(inventory);

            // Log transaction
            var transaction = new InventoryTransaction
            {
                InventoryId = inventory.Id,
                Type = InventoryTransactionType.Outbound,
                Quantity = quantity,
                Reference = reference,
                CreatedAt = DateTime.UtcNow
            };
            await _unitOfWork.InventoryTransactionRepository.AddAsync(transaction);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deducting stock");
            throw;
        }
    }

    public async Task<bool> AddStockAsync(int productId, int quantity, string reference, int? variantId = null)
    {
        try
        {
            var inventories = await _unitOfWork.InventoryRepository.GetAllAsync();
            var inventory = inventories.FirstOrDefault(i =>
                i.ProductId == productId &&
                (variantId == null ? i.ProductVariantId == null : i.ProductVariantId == variantId));

            if (inventory == null)
            {
                throw new KeyNotFoundException("Inventory not found");
            }

            inventory.Quantity += quantity;
            _unitOfWork.InventoryRepository.Update(inventory);

            // Log transaction
            var transaction = new InventoryTransaction
            {
                InventoryId = inventory.Id,
                Type = InventoryTransactionType.Inbound,
                Quantity = quantity,
                Reference = reference,
                CreatedAt = DateTime.UtcNow
            };
            await _unitOfWork.InventoryTransactionRepository.AddAsync(transaction);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding stock");
            throw;
        }
    }

    public async Task<bool> AdjustStockAsync(int productId, int quantity, string notes, int? variantId = null)
    {
        try
        {
            var inventories = await _unitOfWork.InventoryRepository.GetAllAsync();
            var inventory = inventories.FirstOrDefault(i =>
                i.ProductId == productId &&
                (variantId == null ? i.ProductVariantId == null : i.ProductVariantId == variantId));

            if (inventory == null)
            {
                throw new KeyNotFoundException("Inventory not found");
            }

            inventory.Quantity += quantity;
            _unitOfWork.InventoryRepository.Update(inventory);

            // Log transaction
            var transaction = new InventoryTransaction
            {
                InventoryId = inventory.Id,
                Type = InventoryTransactionType.Adjustment,
                Quantity = quantity,
                Reference = $"ADJ-{DateTime.UtcNow.Ticks}",
                Notes = notes,
                CreatedAt = DateTime.UtcNow
            };
            await _unitOfWork.InventoryTransactionRepository.AddAsync(transaction);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adjusting stock");
            throw;
        }
    }

    public async Task<IEnumerable<InventoryTransactionDto>> GetTransactionHistoryAsync(int inventoryId)
    {
        try
        {
            var transactions = await _unitOfWork.InventoryTransactionRepository.GetAllAsync();
            return transactions
                .Where(t => t.InventoryId == inventoryId)
                .OrderByDescending(t => t.CreatedAt)
                .Select(MapTransactionToDto)
                .ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting transaction history");
            throw;
        }
    }

    public async Task<IEnumerable<InventoryDto>> GetLowStockItemsAsync()
    {
        try
        {
            var inventories = await _unitOfWork.InventoryRepository.GetAllAsync();
            return inventories
                .Where(i => i.Quantity - i.ReservedQuantity <= i.LowStockThreshold)
                .Select(MapToDto)
                .ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting low stock items");
            throw;
        }
    }

    private static InventoryDto MapToDto(Inventory inventory)
    {
        return new InventoryDto
        {
            Id = inventory.Id,
            ProductId = inventory.ProductId,
            ProductVariantId = inventory.ProductVariantId,
            Quantity = inventory.Quantity,
            ReservedQuantity = inventory.ReservedQuantity,
            LowStockThreshold = inventory.LowStockThreshold,
            CreatedAt = inventory.CreatedAt,
            UpdatedAt = inventory.UpdatedAt
        };
    }

    private static InventoryTransactionDto MapTransactionToDto(InventoryTransaction transaction)
    {
        return new InventoryTransactionDto
        {
            Id = transaction.Id,
            InventoryId = transaction.InventoryId,
            Type = (int)transaction.Type,
            Quantity = transaction.Quantity,
            Reference = transaction.Reference,
            Notes = transaction.Notes,
            CreatedAt = transaction.CreatedAt
        };
    }
}
