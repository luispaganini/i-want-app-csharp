using IWantApp.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace IWantApp.Infra.Data
{
    public class ApplicationDbContext : DbContext {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Category{ get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<Product>()
                .Property(p => p.Name).IsRequired();

            builder.Entity<Product>()
                .Property(p => p.Description).HasMaxLength(255);

             builder.Entity<Category>()
                .Property(c => c.Name).IsRequired();
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder config) {
            config.Properties<string>()
                .HaveMaxLength(100);
        }
    }
}