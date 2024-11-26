

using AssetsApi.Model;
using Microsoft.EntityFrameworkCore;

namespace AssetsApi.Data
{
    public class AssetsAPIDbContext : DbContext
    {
        public AssetsAPIDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Asset> Assets { get; set; }
    }
}
