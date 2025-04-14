using Microsoft.AspNetCore.Mvc;
using SupplierWebApp.Data;
using SupplierWebApp.Models;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using SupplierWebApp.Models.ViewModels;
using System.Collections.Generic;

namespace SupplierWebApp.Controllers
{
    public class SuppliersController : Controller
    {

        private readonly SupplierDbContext _context;

        public SuppliersController(SupplierDbContext context)
        {
            _context = context;
        }

        // GET: Suppliers
        public IActionResult Index()
        {
            var suppliers = _context.Suppliers.ToList();
            return View(suppliers);
        }

        // GET: Suppliers/Create
        public IActionResult Create()
        {
            var viewModel = new SupplierViewModel
            {
                Supplier = new Supplier(),
                CountryList = GetCountrySelectList()
            };
            return View(viewModel);
        }

        // POST: Suppliers/Create
        [HttpPost]       
        public IActionResult Create(SupplierViewModel viewModel)
        {
         
                viewModel.Supplier.DateCreated = DateTime.Today;
                _context.Suppliers.Add(viewModel.Supplier);
                _context.SaveChanges();
                viewModel.CountryList = GetCountrySelectList();
                return RedirectToAction(nameof(Index));
         
        }

        // GET: Suppliers/Edit/5
        [HttpGet, ActionName("Edit")]
        public IActionResult Edit(int id)
        {
            var supplier = _context.Suppliers.Find(id);
            if (supplier == null)
            {
                return NotFound();
            }

            var viewModel = new SupplierViewModel
            {
                Supplier = supplier,
                CountryList = GetCountrySelectList()
            };
            return View(viewModel);
        }
       

        // POST: Suppliers/Edit/5
        [HttpPost]
       
        public IActionResult Edit(int id, SupplierViewModel viewModel)
        {
            if (id != viewModel.Supplier.Id)
            {
                return NotFound();
            }
          
                try
                {
                    _context.Update(viewModel.Supplier);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Suppliers.Any(e => e.Id == id))
                    {
                        return NotFound();
                    }
                    throw;
                }

                return RedirectToAction(nameof(Index));
         
        }

        // Helper to load countries
        private List<SelectListItem> GetCountrySelectList()
        {
            return _context.CountryMappings
                .Select(c => new SelectListItem
                {
                    Value = c.CountryCode,
                    Text = c.CountryName
                })
                .ToList();
        }
        [HttpGet, ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.Id == id);

            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplier = _context.Suppliers.Find(id);

            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
