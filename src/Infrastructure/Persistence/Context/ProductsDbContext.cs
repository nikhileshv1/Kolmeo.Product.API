using Kolmeo.Domain.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Kolmeo.Infrastructure.Persistence.Context
{
    /// <summary>
    /// ProductsDbContext
    /// </summary>
    public class ProductsDbContext : DbContext
    {
        /// <summary>
        /// DbContext Products table 
        /// </summary>
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// Products DbCOntext Options
        /// </summary>
        /// <param name="options"></param>
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
        {
        }
    }
}
