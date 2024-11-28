using AssetsApi.Data;
using AssetsApi.Model;
using CrudOperationApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace AssetsApiTest
{
    [TestClass]
    public class CategoriesControllerTest
    {
        private AssetsAPIDbContext _dbContext;
        [TestInitialize]
        public void Init()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<AssetsAPIDbContext>().UseSqlite(connection).Options;

            _dbContext = new AssetsAPIDbContext(options);
            
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
        }

        [TestMethod]
        public async Task Test_AddCategory_Async()
        {            
            var controller = new CategoriesController(_dbContext);
            IActionResult result = await controller.AddCategory(new CategoryRequest { Name = "Electronics" });
            var okResult = result as OkObjectResult;
            var category = okResult.Value as Category;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual("Electronics", category.Name);
            
        }

        [TestMethod]
        public async Task Test_GetCategories_Async()
        {
            var controller = new CategoriesController(_dbContext);
            await controller.AddCategory(new CategoryRequest { Name = "Electronics" });
            await controller.AddCategory(new CategoryRequest { Name = "Clothing" });

            IActionResult result = await controller.GetCategories();
            var okResult = result as OkObjectResult;
            var categories = okResult.Value as List<Category>;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(2, categories.Count);

        }

        [TestMethod]
        public async Task Test_GetCategory_Async()
        {
            var controller = new CategoriesController(_dbContext);
            await controller.AddCategory(new CategoryRequest { Name = "Electronics" });
            await controller.AddCategory(new CategoryRequest { Name = "Clothing" });

            IActionResult result = await controller.GetCategory(1);
            var okResult = result as OkObjectResult;
            var category = okResult.Value as Category;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual("Electronics", category.Name);

        }

        [TestMethod]
        public async Task Test_UpdateCategory_Async()
        {
            var controller = new CategoriesController(_dbContext);
            await controller.AddCategory(new CategoryRequest { Name = "Electronics" });
            await controller.AddCategory(new CategoryRequest { Name = "Clothing" });

            IActionResult result = await controller.UpdateCategory(1, new CategoryRequest { Name = "Kitchen"});
            var okResult = result as OkObjectResult;
            var category = okResult.Value as Category;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual("Kitchen", category.Name);

        }

        [TestMethod]
        public async Task Test_DeleteCategory_Async()
        {
            var controller = new CategoriesController(_dbContext);
            await controller.AddCategory(new CategoryRequest { Name = "Electronics" });
            await controller.AddCategory(new CategoryRequest { Name = "Clothing" });

            IActionResult result = await controller.DeleteCategory(1);
            var okResult = result as OkObjectResult;
            var category = okResult.Value as Category;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual("Electronics", category.Name);
            Assert.AreEqual(1, _dbContext.Categories.Count());
        }
    }
}