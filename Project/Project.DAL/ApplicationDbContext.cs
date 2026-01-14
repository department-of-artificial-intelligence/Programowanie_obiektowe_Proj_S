using Microsoft.EntityFrameworkCore;
using Project.Model.Orders;
using Project.Model.People;
using Project.Model.Stores;
using System.Collections.Generic;

namespace Project.DAL 
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Store> Stores { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; } 


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }



        public ApplicationDbContext()
        {
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ElectroHub_Final_Dbv605;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<Customer>().Property(c => c.WalletBalance).HasPrecision(18, 2);
            modelBuilder.Entity<Employee>().Property(e => e.Salary).HasPrecision(18, 2);
            modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(18, 2);


            



            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(o => o.StoreId).IsRequired();

                entity.HasOne(o => o.Store)
                      .WithMany(s => s.Orders) 
                      .HasForeignKey(o => o.StoreId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(o => o.PurchaserId).IsRequired();
                entity.HasOne(o => o.Purchaser)
                      .WithMany() 
                      .HasForeignKey(o => o.PurchaserId)
                      .OnDelete(DeleteBehavior.Restrict);

                
                entity.HasOne(o => o.DeliveryAddress)
                      .WithMany()
                      .HasForeignKey("DeliveryAddressId")
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}