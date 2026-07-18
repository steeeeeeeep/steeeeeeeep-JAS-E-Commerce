using JAS.ECommerce.Application.DTOs.Common;
using JAS.ECommerce.Application.DTOs.Inventory;
using JAS.ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JAS.ECommerce.Presentation.Controllers;

[ApiController]
[Route("api/[controller}")]
[Authorize(Roles = "Admin")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _inventoryService;
    private readonly ILogger<InventoryController> _logger;

    public InventoryController(IInventoryService inventoryService, ILogger<InventoryController> logger)
    {
        _inventoryService = inventoryService;
        _logger = logger;
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetInventory(int productId, [FromQuery] int? variantId = null)
    {
        try
        {
            var inventory = await _inventoryService.GetInventoryAsync(productId, variantId);
            if (inventory == null)
                return NotFound(new { success = false, message = "Inventory not found" });

            return Ok(new { success = true, data = inventory });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting inventory");
            return StatusCode(500, new { success = false, message = "An error occurred" });
        }
    }

    [HttpPost("reserve-stock")]
    public async Task<IActionResult> ReserveStock([FromBody] ReserveStockDto reserveStockDto)
    {
        try
        {
            var result = await _inventoryService.ReserveStockAsync(
                reserveStockDto.ProductId,
                reserveStockDto.Quantity,
                reserveStockDto.VariantId);

            if (!result)
                return BadRequest(new { success = false, message = "Failed to reserve stock" });

            return Ok(new { success = true, message = "Stock reserved successfully" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { success = false, message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reserving stock");
            return StatusCode(500, new { success = false, message = "An error occurred" });
        }
    }

    [HttpPost("release-stock")]
    public async Task<IActionResult> ReleaseStock([FromBody] ReleaseStockDto releaseStockDto)
    {
        try
        {
            var result = await _inventoryService.ReleaseReservedStockAsync(
                releaseStockDto.ProductId,
                releaseStockDto.Quantity,
                releaseStockDto.VariantId);

            if (!result)
                return BadRequest(new { success = false, message = "Failed to release stock" });

            return Ok(new { success = true, message = "Stock released successfully" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { success = false, message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error releasing stock");
            return StatusCode(500, new { success = false, message = "An error occurred" });
        }
    }

    [HttpPost("deduct-stock")]
    public async Task<IActionResult> DeductStock([FromBody] DeductStockDto deductStockDto)
    {
        try
        {
            var result = await _inventoryService.DeductStockAsync(
                deductStockDto.ProductId,
                deductStockDto.Quantity,
                deductStockDto.Reference,
                deductStockDto.VariantId);

            if (!result)
                return BadRequest(new { success = false, message = "Failed to deduct stock" });

            return Ok(new { success = true, message = "Stock deducted successfully" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { success = false, message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deducting stock");
            return StatusCode(500, new { success = false, message = "An error occurred" });
        }
    }

    [HttpPost("add-stock")]
    public async Task<IActionResult> AddStock([FromBody] AddStockDto addStockDto)
    {
        try
        {
            var result = await _inventoryService.AddStockAsync(
                addStockDto.ProductId,
                addStockDto.Quantity,
                addStockDto.Reference,
                addStockDto.VariantId);

            if (!result)
                return BadRequest(new { success = false, message = "Failed to add stock" });

            return Ok(new { success = true, message = "Stock added successfully" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { success = false, message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding stock");
            return StatusCode(500, new { success = false, message = "An error occurred" });
        }
    }

    [HttpPost("adjust-stock")]
    public async Task<IActionResult> AdjustStock([FromBody] AdjustStockDto adjustStockDto)
    {
        try
        {
            var result = await _inventoryService.AdjustStockAsync(
                adjustStockDto.ProductId,
                adjustStockDto.Quantity,
                adjustStockDto.Notes,
                adjustStockDto.VariantId);

            if (!result)
                return BadRequest(new { success = false, message = "Failed to adjust stock" });

            return Ok(new { success = true, message = "Stock adjusted successfully" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { success = false, message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adjusting stock");
            return StatusCode(500, new { success = false, message = "An error occurred" });
        }
    }

    [HttpGet("{inventoryId}/transactions")]
    public async Task<IActionResult> GetTransactionHistory(int inventoryId)
    {
        try
        {
            var transactions = await _inventoryService.GetTransactionHistoryAsync(inventoryId);
            return Ok(new { success = true, data = transactions });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting transaction history");
            return StatusCode(500, new { success = false, message = "An error occurred" });
        }
    }

    [HttpGet("low-stock")]
    public async Task<IActionResult> GetLowStockItems()
    {
        try
        {
            var items = await _inventoryService.GetLowStockItemsAsync();
            return Ok(new { success = true, data = items });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting low stock items");
            return StatusCode(500, new { success = false, message = "An error occurred" });
        }
    }
}

public class ReserveStockDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public int? VariantId { get; set; }
}

public class ReleaseStockDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public int? VariantId { get; set; }
}

public class DeductStockDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string Reference { get; set; } = string.Empty;
    public int? VariantId { get; set; }
}

public class AddStockDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string Reference { get; set; } = string.Empty;
    public int? VariantId { get; set; }
}

public class AdjustStockDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string Notes { get; set; } = string.Empty;
    public int? VariantId { get; set; }
}
