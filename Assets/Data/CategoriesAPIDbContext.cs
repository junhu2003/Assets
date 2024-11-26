

using AssetsApi.Model;
using Microsoft.EntityFrameworkCore;

namespace AssetsApi.Data
{
    public class CategoriesAPIDbContext : DbContext
    {
        public CategoriesAPIDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
    }
}
