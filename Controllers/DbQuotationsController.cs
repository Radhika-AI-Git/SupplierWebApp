using Microsoft.AspNetCore.Mvc;
using SupplierWebApp.Data;
using SupplierWebApp.Models;
using System;
using Microsoft.EntityFrameworkCore;

namespace SupplierWebApp.Controllers
{
    public class DbQuotationsController : Controller
    {

        private readonly SupplierDbContext _context;

        public DbQuotationsController(SupplierDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.DbQuotations.ToListAsync());
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(DbQuotation supplier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var supplier = await _context.DbQuotations.FindAsync(id);
            return supplier == null ? NotFound() : View(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, DbQuotation supplier)
        {
            if (id != supplier.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(supplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _context.DbQuotations.FindAsync(id);
            return supplier == null ? NotFound() : View(supplier);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplier = await _context.DbQuotations.FindAsync(id);
            if (supplier != null)
            {
                _context.DbQuotations.Remove(supplier);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
