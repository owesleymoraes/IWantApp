
using _4_IWantApp.Domain.Products;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace _4_IWantApp.Infra.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)

        {
            builder.Entity<Product>()
             .Property(p => p.Name).IsRequired();

            builder.Entity<Product>()
             .Property(p => p.Description).HasMaxLength(255);

            builder.Entity<Category>()
             .Property(p => p.Name).IsRequired();

            builder.Ignore<Notification>();
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configuration)

        {
            configuration.Properties<string>().HaveMaxLength(100);

        }

    }
}