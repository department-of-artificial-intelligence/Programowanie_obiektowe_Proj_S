using Microsoft.EntityFrameworkCore;
using Project.Model.Orders;
using Project.Model.People;
using Project.Model.Stores;
using System.Collections.Generic;

namespace Project.DAL // <--- Zmiana Namespace
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Store> Stores { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        
        }


       
    }