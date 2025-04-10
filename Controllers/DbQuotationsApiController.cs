using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupplierWebApp.Data;
using SupplierWebApp.Models;
using System;

namespace SupplierWebApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DbQuotationsApiController : ControllerBase
    {
        private readonly SupplierDbContext _context;

        public DbQuotationsApiController(SupplierDbContext context)
        {
            _context = context;
        }

        // GET: api/DbQuotations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DbQuotation>>> GetSuppliers()
        {
            return await _context.DbQuotations.ToListAsync();
        }

        // GET: api/DbQuotations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DbQuotation>> GetSupplier(int id)
        {
            var quotation = await _context.DbQuotations.FindAsync(id);
            return quotation == null ? NotFound() : quotation;
        }

        // POST: api/DbQuotations
        [HttpPost]
        public async Task<ActionResult<DbQuotation>> CreateSupplier(DbQuotation supplier)
        {
            _context.DbQuotations.Add(supplier);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSupplier), new { id = supplier.Id }, supplier);
        }

        // PUT: api/DbQuotations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, DbQuotation supplier)
        {
            if (id != supplier.Id) return BadRequest();

            _context.Entry(supplier).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.DbQuotations.Any(e => e.Id == id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/DbQuotations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var quotation = await _context.DbQuotations.FindAsync(id);
            if (quotation == null) return NotFound();

            _context.DbQuotations.Remove(quotation);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}