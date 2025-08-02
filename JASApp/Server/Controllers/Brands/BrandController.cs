using JAS.Shared.Dto.Brand;
using JASApi.Data;
using JASData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace JASApp.Api.Controllers.Brands;

[Route("api/[controller]")]
[ApiController]
public class BrandController(AppDbContext context) : Controller
{
    private readonly AppDbContext _dbContext = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrandsAsync()
    {
        try
        {
            List<Brand> Brands = await _dbContext.Brands.Where(c => !c.IsDeleted).ToListAsync();

            if (Brands.IsNullOrEmpty())
                return new List<BrandDto>();

            List<BrandDto> brandDtos = Brands.Select(c => new BrandDto()
            {
                BrandId = c.BrandId,
                BrandName = c.BrandName,
                Description = c.Description,
                IsFeatured = c.IsFeatured,
            }).ToList();


            return brandDtos;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BrandDto>> GetBrandByIdAsync(int id)
    {
        try
        {
            Brand brand = await _dbContext.Brands.Where(c => !c.IsDeleted).FirstOrDefaultAsync(c => c.BrandId == id);

            if (brand == null)
                return new BrandDto();

            BrandDto brandDto = new BrandDto()
            {
                BrandId = brand.BrandId,
                BrandName = brand.BrandName,
                Description = brand.Description,
                IsFeatured = brand.IsFeatured,
            };


            return brandDto;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<bool>> CreateBrandAsync(AddOrUpdateBrandDto dto)
    {
        try
        {
            Brand getBrand = await _dbContext.Brands.Where(c => !c.IsDeleted).FirstOrDefaultAsync(c => c.BrandId == dto.BrandId);

            if (getBrand != null)
                return false;

            Brand brand = new Brand()
            {
                BrandId = dto.BrandId,
                BrandName = dto.BrandName,
                Description = dto.Description,
                IsFeatured = dto.IsFeatured,
                CreatedOn = DateTime.UtcNow,
            };

            await _dbContext.Brands.AddAsync(brand);
            int result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<bool>> UpdateBrandAsync(int id, AddOrUpdateBrandDto dto)
    {
        try
        {
            Brand getBrand = await _dbContext.Brands.Where(c => !c.IsDeleted).FirstOrDefaultAsync(c => c.BrandId == id);

            if (getBrand == null)
                return false;

            getBrand.BrandId = dto.BrandId;
            getBrand.BrandName = dto.BrandName;
            getBrand.Description = dto.Description;
            getBrand.IsFeatured = dto.IsFeatured;
            getBrand.UpdatedOn = DateTime.UtcNow;

            _dbContext.Brands.Update(getBrand);
            int result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("delete/{id}")]
    public async Task<ActionResult<bool>> DeleteBrandAsync(int id, AddOrUpdateBrandDto dto)
    {
        try
        {
            Brand getBrand = await _dbContext.Brands.Where(c => !c.IsDeleted).FirstOrDefaultAsync(c => c.BrandId == id);

            if (getBrand == null)
                return false;

            getBrand.IsDeleted = true;
            getBrand.DeletedOn = DateTime.UtcNow;

            _dbContext.Brands.Update(getBrand);
            int result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
