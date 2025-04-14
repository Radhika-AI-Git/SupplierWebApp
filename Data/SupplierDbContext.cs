using Microsoft.EntityFrameworkCore;
using SupplierWebApp.Models;
using System;

namespace SupplierWebApp.Data
{
    public class SupplierDbContext :DbContext
    {
        public SupplierDbContext(DbContextOptions<SupplierDbContext> options) : base(options) { }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Quotation> Quotations { get; set; }
        public DbSet<CountryMapping> CountryMappings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Supplier>()
                .HasMany(s => s.Quotations)
                .WithOne(q => q.Supplier)
                .HasForeignKey(q => q.SupplierId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
