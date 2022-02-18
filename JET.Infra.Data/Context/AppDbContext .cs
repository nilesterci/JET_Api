using JET.Domain.Entities.Tables;
using Microsoft.EntityFrameworkCore;

namespace JET.Infra.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Products> Products { get; set; }
    }
}
