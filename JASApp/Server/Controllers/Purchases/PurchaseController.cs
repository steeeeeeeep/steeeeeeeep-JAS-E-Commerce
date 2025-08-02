using JAS.Shared.Dto.Purchase;
using JASApi.Data;
using JASData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace JASApp.Api.Controllers.Purchases;

[Route("api/[controller]")]
[ApiController]
public class PurchaseController(AppDbContext context) : Controller
{
    private readonly AppDbContext _dbContext = context;



    [HttpGet]
    public async Task<ActionResult<IEnumerable<PurchaseDto>>> GetAllPurchasesAsync()
    {
        try
        {
            List<Purchase> Purchases = await _dbContext.Purchases.Where(c => !c.IsDeleted).ToListAsync();

            if (Purchases.IsNullOrEmpty())
                return new List<PurchaseDto>();

            List<PurchaseDto> PurchaseDtos = Purchases.Select(c => new PurchaseDto()
            {
                PurchaseId = c.PurchaseId,
                Name = c.Name,
                Price = c.Price,
                Quantity = c.Quantity,
            }).ToList();


            return PurchaseDtos;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PurchaseDto>> GetPurchaseByIdAsync(int id)
    {
        try
        {
            Purchase Purchase = await _dbContext.Purchases.Where(c => !c.IsDeleted).FirstOrDefaultAsync(c => c.PurchaseId == id);

            if (Purchase == null)
                return new PurchaseDto();

            PurchaseDto PurchaseDto = new PurchaseDto()
            {
                PurchaseId = Purchase.PurchaseId,
                Name = Purchase.Name,
                Price = Purchase.Price,
                Quantity = Purchase.Quantity,
            };


            return PurchaseDto;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<bool>> CreatePurchaseAsync(PurchaseDto dto)
    {
        try
        {
            Purchase getPurchase = await _dbContext.Purchases.Where(c => !c.IsDeleted).FirstOrDefaultAsync(c => c.PurchaseId == dto.PurchaseId);

            if (getPurchase != null)
                return false;

            Purchase Purchase = new Purchase()
            {
                PurchaseId = dto.PurchaseId,
                Name = dto.Name,
                Price = dto.Price,
                Quantity = dto.Quantity,
                CreatedOn = DateTime.UtcNow,
            };

            await _dbContext.Purchases.AddAsync(Purchase);
            int result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<bool>> UpdatePurchaseAsync(int id, PurchaseDto dto)
    {
        try
        {
            Purchase getPurchase = await _dbContext.Purchases.Where(c => !c.IsDeleted).FirstOrDefaultAsync(c => c.PurchaseId == id);

            if (getPurchase == null)
                return false;

            getPurchase.PurchaseId = dto.PurchaseId;
            getPurchase.Name = dto.Name;
            getPurchase.Price = dto.Price;
            getPurchase.Quantity = dto.Quantity;
            getPurchase.UpdatedOn = DateTime.UtcNow;

            _dbContext.Purchases.Update(getPurchase);
            int result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("delete/{id}")]
    public async Task<ActionResult<bool>> DeletePurchaseAsync(int id, PurchaseDto dto)
    {
        try
        {
            Purchase getPurchase = await _dbContext.Purchases.Where(c => !c.IsDeleted).FirstOrDefaultAsync(c => c.PurchaseId == id);

            if (getPurchase == null)
                return false;

            getPurchase.IsDeleted = true;
            getPurchase.DeletedOn = DateTime.UtcNow;

            _dbContext.Purchases.Update(getPurchase);
            int result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
