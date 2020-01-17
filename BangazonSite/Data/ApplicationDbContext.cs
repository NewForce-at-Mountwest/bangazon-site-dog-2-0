using System;
using System.Collections.Generic;
using System.Text;
using BangazonSite.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BangazonSite.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<PaymentType>().HasMany(paymentType => paymentType.Orders)
                        .WithOne(orders => orders.PaymentType)
                        .OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasMany(product => product.OrderProducts)
                       .WithOne(orderProducts => orderProducts.Product)
                       .OnDelete(DeleteBehavior.Restrict);
        }
        


    }
}
