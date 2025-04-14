using Microsoft.AspNetCore.Mvc;
using SupplierWebApp.Data;
using SupplierWebApp.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace SupplierWebApp.Controllers
{
    public class QuotationsController : Controller
    {

        private readonly SupplierDbContext _context;
        public Dictionary<string, List<Quotation>> SortedSuppliers { get; set; } = new();
        public QuotationsController(SupplierDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var quotations = await _context.Quotations
        .Include(q => q.Supplier)
        .Where(q => q.CostPerUnit.HasValue) // filters out nulls
        .ToListAsync();
            return View(quotations);
        }

        public IActionResult Create() => View();
        [HttpGet]
        public IActionResult Create(int supplierId)
        {
            var quotation = new Quotation
            {
                SupplierId = supplierId
            };

            return View(quotation);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Quotation quotation)
        {
             _context.Add(quotation);
                await _context.SaveChangesAsync();
              return RedirectToAction(nameof(Index));
         

        }

        public async Task<IActionResult> Edit(int id)
        {
            var supplier = await _context.Quotations.FindAsync(id);
            return supplier == null ? NotFound() : View(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Quotation supplier)
        {
            if (id != supplier.Id) return NotFound();
            {
                _context.Update(supplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

                return View(supplier);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _context.Quotations.FindAsync(id);
            return supplier == null ? NotFound() : View(supplier);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplier = await _context.Quotations.FindAsync(id);
            if (supplier != null)
            {
                _context.Quotations.Remove(supplier);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

      

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

        public async Task<IActionResult> ExportToCsv()
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
