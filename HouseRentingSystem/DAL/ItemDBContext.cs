using Castle.Core.Resource;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HouseRentingSystem.Models;


namespace HouseRentingSystem.DAL
{

    public class ItemDbContext : IdentityDbContext
    {
        public ItemDbContext(DbContextOptions<ItemDbContext> options) : base(options)
        {
     
        }

        public DbSet<User> user { get; set; }
        public DbSet<House> house { get; set; }
        public DbSet<Customer> customer { get; set; }
        public DbSet<Order> order { get; set; }
        public DbSet<Owner> owner { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin<string>>()
           .HasKey(l => new { l.LoginProvider, l.ProviderKey });
            modelBuilder.Entity<Customer>().ToTable("customer");
            modelBuilder.Entity<Owner>().ToTable("owner");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}