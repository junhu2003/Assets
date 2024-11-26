﻿using AssetsApi.Data;
using AssetsApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudOperationApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AssetsController : ControllerBase
    {
        private AssetsAPIDbContext dbContext;
        public AssetsController(AssetsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAssets()
        {
            return Ok(await dbContext.Assets.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetAsset([FromRoute] Guid id)
        {
            var asset = await dbContext.Assets.FindAsync(id);
            if (asset != null)
            {                
                return Ok(asset);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsset(AssetRequest assetRequest)
        {
            var asset = new Asset()
            {
                Id = Guid.NewGuid(),
                Name = assetRequest.Name,
                Value = assetRequest.Value,
                CategoryId = assetRequest.CategoryId,                
            };
            await dbContext.Assets.AddAsync(asset);
            await dbContext.SaveChangesAsync();
            return Ok(asset);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateAsset([FromRoute] Guid id, AssetRequest assetRequest)
        {
            var asset = await dbContext.Assets.FindAsync(id);
            if (asset != null)
            {
                asset.Name = assetRequest.Name;
                asset.Value = assetRequest.Value;
                asset.CategoryId = assetRequest.CategoryId;
                
                await dbContext.SaveChangesAsync();
                return Ok(asset);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteAsset(Guid id)
        {
            var asset = await dbContext.Assets.FindAsync(id);
            if (asset != null)
            {
                dbContext.Remove(asset);
                dbContext.SaveChanges();
                return Ok(asset);
            }
            return NotFound();
        }
    }
}