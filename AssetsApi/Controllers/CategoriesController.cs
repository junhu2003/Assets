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
        private AssetsAPIDbContext dbContext;
        public CategoriesController(AssetsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await dbContext.Categories.ToListAsync());
        }

        [HttpGet]
        [Route("{id:long}")]
        public async Task<IActionResult> GetCategory([FromRoute] long id)
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
                Name = categoryRequest.Name,                
            };
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return Ok(category);
        }

        [HttpPut]
        [Route("{id:long}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] long id, CategoryRequest categoryRequest)
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
        [Route("{id:long}")]
        public async Task<IActionResult> DeleteCategory(long id)
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
