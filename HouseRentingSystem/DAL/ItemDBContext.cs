using Castle.Core.Resource;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HouseRentingSystem.Models;
using System.Reflection.Emit;


namespace HouseRentingSystem.DAL
{

    public class ItemDBContext : IdentityDbContext
    {
        public ItemDBContext(DbContextOptions<ItemDBContext> options) : base(options)
        {
     
        }

        public DbSet<User> User { get; set; }
        public DbSet<House> House { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Owner> Owner { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin<string>>()
               .HasKey(l => new { l.LoginProvider, l.ProviderKey });
            modelBuilder.Entity<Customer>().ToTable("customer");
            modelBuilder.Entity<Owner>().ToTable("owner");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder OptionsBuilder)
        {
            OptionsBuilder.UseLazyLoadingProxies();
        }

    }
}