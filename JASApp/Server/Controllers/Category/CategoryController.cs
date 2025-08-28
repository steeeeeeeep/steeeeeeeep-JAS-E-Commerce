using JAS.Shared.Dto.ProductCategory;
using JASApp.Api.Data;
using JASData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JASApp.Api.Controllers.Category;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(AppDbContext context) : Controller
{
    protected readonly AppDbContext _dbContext = context;

    [HttpGet("All")]
    public async Task<ActionResult<IEnumerable<ProductCategoryGetDto>>> GetAllCategoryAsync()
    {
        try
        {
            List<ProductCategory> categories = await _dbContext.ProductCategories.Where(x => !x.IsDeleted).ToListAsync();
            List<ProductCategoryGetDto> productListDto = categories.Select(model => new ProductCategoryGetDto
            {
                ProductCategoryId = model.ProductCategoryId,
                Name = model.Name,
                Description = model.Description,
                IsFeatured = model.Featured
            }).ToList();

            return productListDto;
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("Get/{id}")]
    public async Task<ActionResult<ProductCategoryGetDto>> GetCategoryByIdAsync(int id)
    {
        try
        {
            ProductCategory model = await _dbContext.ProductCategories.Where(x => x.ProductCategoryId == id).FirstOrDefaultAsync();

            if (model == null)
                return new ProductCategoryGetDto();

            ProductCategoryGetDto productDto = new ProductCategoryGetDto
            {
                ProductCategoryId = model.ProductCategoryId,
                Name = model.Name,
                Description = model.Description,
                IsFeatured = model.Featured
            };

            return productDto;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<bool>> CreateCategoryAsync(ProductCategoryCreateOrUpdateDto updateCategoryDto)
    {
        try
        {
            if (updateCategoryDto.ProductCategoryId != 0)
                return false;

            ProductCategory productCategory = new ProductCategory()
            {
                ProductCategoryId = updateCategoryDto.ProductCategoryId,
                Name = updateCategoryDto.Name,
                Description = updateCategoryDto.Description,
                Featured = updateCategoryDto.IsFeatured,
                CreatedOn = DateTime.UtcNow
            };

            _dbContext.Update(productCategory);
            int result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("Update/{id}")]
    public async Task<ActionResult<bool>> UpdateCategoryAsync(int id, ProductCategoryCreateOrUpdateDto updateCategory)
    {
        try
        {
            ProductCategory model = await _dbContext.ProductCategories.Where(x => x.ProductCategoryId == id).FirstOrDefaultAsync();

            if (model == null)
                return false;

            model.ProductCategoryId = updateCategory.ProductCategoryId;
            model.Name = updateCategory.Name;
            model.Description = updateCategory.Description;
            model.Featured = updateCategory.IsFeatured;

            _dbContext.Update(model);
            int result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("Delete/{id}")]
    public async Task<ActionResult<bool>> DeleteCategoryAsync(int id)
    {
        try
        {
            ProductCategory model = await _dbContext.ProductCategories.Where(x => x.ProductCategoryId == id).FirstOrDefaultAsync();

            if (model == null)
                return false;

            model.DeletedOn = DateTime.Now;
            model.IsDeleted = true;

            _dbContext.Update(model);
            int result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
