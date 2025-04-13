using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SupplierWebApp.Data;
using System.Globalization;
using System.Text;
using System.IO;
using Microsoft.EntityFrameworkCore;
namespace SupplierWebApp.Models.ViewModels
{
    public class QuotationsModel
    {
        private readonly SupplierDbContext _context;

        public QuotationsModel(SupplierDbContext context)
        {
            _context = context;
        }

        public Dictionary<string, List<Quotation>> SortedSuppliers { get; set; } = new();

        public async Task OnGetAsync()
        {
            var data = await _context.Quotations
                .Include(q => q.Supplier)
                .Where(q => q.CostPerUnit != null)
                .ToListAsync();

            SortedSuppliers = data
                .GroupBy(q => q.Supplier.Name)
                .OrderByDescending(g => g.Key)
                .ToDictionary(
                    g => g.Key,
                    g => g.OrderByDescending(q => q.Product).ToList()
                );
        }

        public async Task<IActionResult> OnPostExportCsvAsync()
        {
            var data = await _context.Quotations
                .Include(q => q.Supplier)
                .Where(q => q.CostPerUnit != null)
                .ToListAsync();

            var sorted = data
                .GroupBy(q => q.Supplier.Name)
                .OrderByDescending(g => g.Key)
                .ToDictionary(
                    g => g.Key,
                    g => g.OrderByDescending(q => q.Product).ToList()
                );

            var sb = new StringBuilder();
            sb.AppendLine("Supplier Name,Product,CostPerUnit");

            foreach (var supplier in sorted)
            {
                foreach (var q in supplier.Value)
                {
                    sb.AppendLine($"{supplier.Key},{q.Product},{q.CostPerUnit}");
                }
            }

            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return new FileContentResult(bytes, "text/csv")
            {
                FileDownloadName = "Quotations.csv"
            };

        }

    }
}