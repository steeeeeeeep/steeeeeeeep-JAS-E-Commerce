using JAS.Shared.Dto.Inventory;
using JASApp.Api.Data;
using JASData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace JASApp.Api.Controllers.Inventories;

[Route("api/[controller]")]
[ApiController]
public class InventoryController(AppDbContext context) : Controller
{
    private readonly AppDbContext _dbContext = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InventoryDto>>> GetAllInventorysAsync()
    {
        try
        {
            List<Inventory> Inventorys = await _dbContext.Inventories.Where(c => !c.IsDeleted).ToListAsync();

            if (Inventorys.IsNullOrEmpty())
                return new List<InventoryDto>();

            List<InventoryDto> InventoryDtos = Inventorys.Select(c => new InventoryDto()
            {
                Id = c.Id,
                ProductId = c.ProductId,
                Quantity = c.Quantity,
            }).ToList();


            return InventoryDtos;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InventoryDto>> GetInventoryByIdAsync(int id)
    {
        try
        {
            Inventory Inventory = await _dbContext.Inventories.Where(c => !c.IsDeleted).FirstOrDefaultAsync(c => c.Id == id);

            if (Inventory == null)
                return new InventoryDto();

            InventoryDto InventoryDto = new InventoryDto()
            {
                Id = Inventory.Id,
                Quantity = Inventory.Quantity,
                ProductId = Inventory.ProductId,
            };


            return InventoryDto;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<bool>> CreateInventoryAsync(AddOrUpdateInventoryDto dto)
    {
        try
        {
            Inventory getInventory = await _dbContext.Inventories.Where(c => !c.IsDeleted).FirstOrDefaultAsync(c => c.Id == dto.Id);

            if (getInventory != null)
                return false;

            Inventory Inventory = new Inventory()
            {
                Id = dto.Id,
                ProductId= dto.ProductId,
                Quantity = dto.Quantity,
                CreatedOn = DateTime.UtcNow,
            };

            await _dbContext.Inventories.AddAsync(Inventory);
            int result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<bool>> UpdateInventoryAsync(int id, AddOrUpdateInventoryDto dto)
    {
        try
        {
            Inventory getInventory = await _dbContext.Inventories.Where(c => !c.IsDeleted).FirstOrDefaultAsync(c => c.Id == id);

            if (getInventory == null)
                return false;

            getInventory.Id = dto.Id;
            getInventory.Quantity = dto.Quantity;
            getInventory.ProductId = dto.ProductId;
            getInventory.UpdatedOn = DateTime.UtcNow;

            _dbContext.Inventories.Update(getInventory);
            int result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("delete/{id}")]
    public async Task<ActionResult<bool>> DeleteInventoryAsync(int id, AddOrUpdateInventoryDto dto)
    {
        try
        {
            Inventory getInventory = await _dbContext.Inventories.Where(c => !c.IsDeleted).FirstOrDefaultAsync(c => c.Id == id);

            if (getInventory == null)
                return false;

            getInventory.IsDeleted = true;
            getInventory.DeletedOn = DateTime.UtcNow;

            _dbContext.Inventories.Update(getInventory);
            int result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
