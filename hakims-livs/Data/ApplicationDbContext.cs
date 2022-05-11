using hakims_livs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace hakims_livs.Data;

public class ApplicationDbContext : IdentityDbContext<Customer>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<Address> Adresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
        const string ROLE_ID = "ad376a8f-9eab-4bb9-9fca-30b01540f445";

        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = ROLE_ID,
            Name = "admin",
            NormalizedName = "admin"
        });

        var hasher = new PasswordHasher<IdentityUser>();
        var adminUser = modelBuilder.Entity<Customer>().HasData(new IdentityUser
        {
            Id = ADMIN_ID,
            UserName = "admin@gmail.com",
            NormalizedUserName = "admin@gmail.com",
            Email = "admin@gmail.com",
            NormalizedEmail = "admin@gmail.com",
            EmailConfirmed = true,
            PasswordHash = hasher.HashPassword(null, "Admin123#"),
            SecurityStamp = string.Empty
        });

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = ROLE_ID,
            UserId = ADMIN_ID
        });

        modelBuilder.Entity<Product>().HasMany(p => p.Categories);
    }

    public DbSet<hakims_livs.Models.OrderRow> OrderRow { get; set; }
}

