using System;
using System.Collections.Generic;
using System.Text;
using BangazonSite.Models;
using Microsoft.AspNetCore.Identity;
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
            base.OnModelCreating(modelBuilder);
            // Create a new user for Identity Framework
            ApplicationUser user = new ApplicationUser
            {
                FirstName = "admin",
                LastName = "admin",
                StreetAddress = "123 Infinity Way",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794577",
                Id = "00000000-ffff-ffff-ffff-ffffffffffff"
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user);
            modelBuilder.Entity<PaymentType>().HasMany(paymentType => paymentType.Orders)
                        .WithOne(orders => orders.PaymentType)
                        .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Product>().HasMany(product => product.OrderProducts)
            //           .WithOne(orderProducts => orderProducts.Product)
            //           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>().HasMany(order => order.OrderProducts)
                         .WithOne(orderProducts => orderProducts.Order)
                         .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Product>().HasMany(product => product.OrderProducts)
                       .WithOne(orderProducts => orderProducts.Product)
                       .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .Property(D => D.DateCreated)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<ProductType>().HasData(
            new ProductType()
            {
                Id = 1,
                Name = "Technology"
            });

            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    DateCreated = new DateTime(2020, 01, 01),
                    Description = "Cool Tech",
                    Title = "Computer",
                    Price = 500.00,
                    Quantity = 1,
                    UserId = "00000000-ffff-ffff-ffff-ffffffffffff",
                    City = "Huntington",
                    LocalDelivery = true,
                    ProductTypeId = 1
                });
            modelBuilder.Entity<PaymentType>().HasData(
                new PaymentType()
                {
                    Id = 3,
                    AccountNumber = 333,
                    Name = "Amex",
                    ApplicationUserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                });

            modelBuilder.Entity<Order>().HasData(
            new Order()
            {
                Id = 1,
                PaymentTypeId = 3,
                ApplicationUserId = "00000000-ffff-ffff-ffff-ffffffffffff"
            });
            //new Order()
            //{
            //    Id = 2,
            //    PaymentTypeId = null,
            //    ApplicationUserId = "00000000-ffff-ffff-ffff-ffffffffffff"
            //};

            modelBuilder.Entity<OrderProduct>().HasData(
            new OrderProduct()
            {
                Id = 1,
                OrderId = 1,
                ProductId = 1
            });
        }



    }
}
