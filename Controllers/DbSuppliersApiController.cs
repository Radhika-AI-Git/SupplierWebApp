using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupplierWebApp.Data;
using SupplierWebApp.Models;
using System;

namespace SupplierWebApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DbSuppliersApiController : ControllerBase
    {
        private readonly SupplierDbContext _context;

        public DbSuppliersApiController(SupplierDbContext context)
        {
            _context = context;
        }

        // GET: api/DbSuppliers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DbSupplier>>> GetSuppliers()
        {
            return await _context.DbSuppliers.ToListAsync();
        }

        // GET: api/DbSuppliers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DbSupplier>> GetSupplier(int id)
        {
            var supplier = await _context.DbSuppliers.FindAsync(id);
            return supplier == null ? NotFound() : supplier;
        }

        // POST: api/DbSuppliers
        [HttpPost]
        public async Task<ActionResult<DbSupplier>> CreateSupplier(DbSupplier supplier)
        {
            _context.DbSuppliers.Add(supplier);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSupplier), new { id = supplier.Id }, supplier);
        }

        // PUT: api/DbSuppliers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, DbSupplier supplier)
        {
            if (id != supplier.Id) return BadRequest();

            _context.Entry(supplier).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.DbSuppliers.Any(e => e.Id == id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/DbSuppliers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var supplier = await _context.DbSuppliers.FindAsync(id);
            if (supplier == null) return NotFound();

            _context.DbSuppliers.Remove(supplier);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}