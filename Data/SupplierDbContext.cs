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
       
    }
}
