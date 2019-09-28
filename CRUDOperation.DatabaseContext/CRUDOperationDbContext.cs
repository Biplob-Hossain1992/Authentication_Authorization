using CRUDOperation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDOperation.DatabaseContext
{
    public class CRUDOperationDbContext:IdentityDbContext<IdentityUser> //use for authentication
    {
        public long CurrentUserId { get; set; }

        public CRUDOperationDbContext(DbContextOptions options):base(options)
        {

        }

        public CRUDOperationDbContext()
        {
                
        }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Stock> Stocks { get; set; }

        //public DbSet<Order> Orders { get; set; }
        //public object Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies(false)
                .UseSqlServer("Server=(local);Database=CRUDOperation_Auth; Integrated Security=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);//used for authentication
            modelBuilder.Entity<Product>().HasQueryFilter(p => p.IsActive);
        }

    }
}
