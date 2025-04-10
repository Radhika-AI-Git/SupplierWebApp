using Microsoft.AspNetCore.Mvc;
using SupplierWebApp.Data;
using SupplierWebApp.Models;
using System;
using Microsoft.EntityFrameworkCore;

namespace SupplierWebApp.Controllers
{
    public class DbSuppliersController : Controller
    {

        private readonly SupplierDbContext _context;

        public DbSuppliersController(SupplierDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.DbSuppliers.ToListAsync());
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(DbSupplier supplier)
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
            var supplier = await _context.DbSuppliers.FindAsync(id);
            return supplier == null ? NotFound() : View(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, DbSupplier supplier)
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
            var supplier = await _context.DbSuppliers.FindAsync(id);
            return supplier == null ? NotFound() : View(supplier);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplier = await _context.DbSuppliers.FindAsync(id);
            if (supplier != null)
            {
                _context.DbSuppliers.Remove(supplier);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
