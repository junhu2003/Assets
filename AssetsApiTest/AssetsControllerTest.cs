using AssetsApi.Data;
using AssetsApi.Model;
using CrudOperationApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace AssetsApiTest
{
    [TestClass]
    public class AssetsControllerTest
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
        public async Task Test_AddAsset_Async()
        {            
            var controller = new AssetsController(_dbContext);
            IActionResult result = await controller.AddAsset(new AssetRequest { Name = "Laptop", Value=2000, CategoryId=1 });
            var okResult = result as OkObjectResult;
            var asset = okResult.Value as Asset;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual("Laptop", asset.Name);
            
        }

        [TestMethod]
        public async Task Test_GetAssets_Async()
        {
            var controller = new AssetsController(_dbContext);
            await controller.AddAsset(new AssetRequest { Name = "Laptop", Value = 2000, CategoryId = 1 });
            await controller.AddAsset(new AssetRequest { Name = "Desktop", Value = 1800, CategoryId = 1 });

            IActionResult result = await controller.GetAssets();
            var okResult = result as OkObjectResult;
            var assets = okResult.Value as List<Asset>;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(2, assets.Count);

        }

        [TestMethod]
        public async Task Test_GetAsset_Async()
        {
            var controller = new AssetsController(_dbContext);
            await controller.AddAsset(new AssetRequest { Name = "Laptop", Value = 2000, CategoryId = 1 });
            await controller.AddAsset(new AssetRequest { Name = "Desktop", Value = 1800, CategoryId = 2 });

            IActionResult result = await controller.GetAsset(2);
            var okResult = result as OkObjectResult;
            var asset = okResult.Value as Asset;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual("Desktop", asset.Name);

        }

        [TestMethod]
        public async Task Test_UpdateAsset_Async()
        {
            var controller = new AssetsController(_dbContext);
            await controller.AddAsset(new AssetRequest { Name = "Laptop", Value = 2000, CategoryId = 1 });
            await controller.AddAsset(new AssetRequest { Name = "Desktop", Value = 1800, CategoryId = 2 });

            IActionResult result = await controller.UpdateAsset(1, new AssetRequest { Name = "Computer", Value = 1800, CategoryId = 2 });
            var okResult = result as OkObjectResult;
            var asset = okResult.Value as Asset;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual("Computer", asset.Name);

        }

        [TestMethod]
        public async Task Test_DeleteAsset_Async()
        {
            var controller = new AssetsController(_dbContext);
            await controller.AddAsset(new AssetRequest { Name = "Laptop", Value = 2000, CategoryId = 1 });
            await controller.AddAsset(new AssetRequest { Name = "Desktop", Value = 1800, CategoryId = 2 });

            IActionResult result = await controller.DeleteAsset(1);
            var okResult = result as OkObjectResult;
            var asset = okResult.Value as Asset;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual("Laptop", asset.Name);
            Assert.AreEqual(1, _dbContext.Assets.Count());
        }
    }
}