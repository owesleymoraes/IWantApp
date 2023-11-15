
using _4_IWantApp.Domain.Products;
using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using _4_IWantApp.Domain.Orders;

namespace _4_IWantApp.Infra.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)

        {

            // chama a classe pai que nesse caso é IdentityDbContext
            // caso não tivesse daria um erro : IdentityUserLogin<string>' requires a primary key
            base.OnModelCreating(builder);

            builder.Entity<Product>()
             .Property(p => p.Name).IsRequired();

            builder.Entity<Product>()
             .Property(p => p.Description).HasMaxLength(255);

            builder.Entity<Product>()
             .Property(p => p.Price).HasColumnType("decimal(10,2)").IsRequired();

            builder.Entity<Category>()
             .Property(c => c.Name).IsRequired();

            builder.Entity<Order>()
             .Property(o => o.ClientId).IsRequired();

            builder.Entity<Order>()
             .Property(o => o.DeliveryAddress).IsRequired();

            builder.Entity<Order>()
             .HasMany(o => o.Products)
             .WithMany(o => o.Orders)
             .UsingEntity(x => x.ToTable("OrderProduct"));

            builder.Ignore<Notification>();
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configuration)

        {
            configuration.Properties<string>().HaveMaxLength(100);

        }

    }
}