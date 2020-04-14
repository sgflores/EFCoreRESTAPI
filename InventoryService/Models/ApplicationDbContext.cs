using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {

        }

        public DbSet<Products> Products { get; set; }

        public DbSet<UserInfo> UserInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>().HasData(
               new Products
               {
                   Id = -1,
                  Name = "Corned Beed",
                  Category = "Can",
                  Color = "Yellow",
                  UnitPrice = 100,
                  AvailableQuantity = 1
               }
            );

            #region UserInfoSeed
            modelBuilder.Entity<UserInfo>().HasData(
                new UserInfo {
                    Id = -1,
                    FirstName = "Inventory", 
                    LastName = "Admin", 
                    UserName = "InventoryAdmin", 
                    Email = "InventoryAdmin@abc.com", 
                    Password = "$admin@2017"
                }
            );
            #endregion
        }
    }
}
