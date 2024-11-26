using AssetsApi.Data;
using AssetsApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudOperationApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CategoriesController : ControllerBase
    {
        private CategoriesAPIDbContext dbContext;
        public CategoriesController(CategoriesAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await dbContext.Categories.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetCategory([FromRoute] Guid id)
        {
            var category = await dbContext.Categories.FindAsync(id);
            if (category != null)
            {                
                return Ok(category);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryRequest categoryRequest)
        {
            var category = new Category()
            {
                Id = Guid.NewGuid(),
                Name = categoryRequest.Name,                
            };
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return Ok(category);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, CategoryRequest categoryRequest)
        {
            var category = await dbContext.Categories.FindAsync(id);
            if (category != null)
            {
                category.Name = categoryRequest.Name;                
                
                await dbContext.SaveChangesAsync();
                return Ok(category);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await dbContext.Categories.FindAsync(id);
            if (category != null)
            {
                dbContext.Remove(category);
                dbContext.SaveChanges();
                return Ok(category);
            }
            return NotFound();
        }
    }
}
