using Microsoft.EntityFrameworkCore;
using SupplierWebApp.Models;
using System;

namespace SupplierWebApp.Data
{
    public class SupplierDbContext :DbContext
    {
        public SupplierDbContext(DbContextOptions<SupplierDbContext> options) : base(options) { }

        public DbSet<DbSupplier> DbSuppliers { get; set; }
        public DbSet<DbQuotation> DbQuotations { get; set; }
    }
}
