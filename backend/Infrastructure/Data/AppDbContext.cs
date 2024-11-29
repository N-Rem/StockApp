using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<SupplierProduct> SuppliersProducts { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<HistoricalPrice> HistoricalPrices { get; set; }
        public DbSet<BranchTransaction> BranchTransactions { get; set; }
        public DbSet<Branch> Branches { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne<User>() //Tiene un Dueño
                .WithMany()
                .HasForeignKey(u => u.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Type)
                    .HasConversion<string>() // Convierte el enum a un string
                    .HasMaxLength(20); //Define un tamaño máximo
            });

            modelBuilder.Entity<Branch>()
                .HasOne<User>() //Tiene un dueño
                .WithMany()
                .HasForeignKey(b => b.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HistoricalPrice>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(hp => hp.ProductId);

            modelBuilder.Entity<SupplierProduct>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(sp => sp.ProductId);

            modelBuilder.Entity<SupplierProduct>()
                .HasOne<Supplier>()
                .WithMany()
                .HasForeignKey(sp => sp.SupplierId);

            modelBuilder.Entity<Transaction>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(t => t.ProductId)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BranchTransaction>()
                .HasOne<Branch>()
                .WithMany()
                .HasForeignKey(bt => bt.BranchId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BranchTransaction>()
                .HasOne<Transaction>()
                .WithMany()
                .HasForeignKey(bt => bt.TransactionId)
                .OnDelete(DeleteBehavior.Cascade);

        }






    }
}
