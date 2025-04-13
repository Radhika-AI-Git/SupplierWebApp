using System.ComponentModel.DataAnnotations;

namespace SupplierWebApp.Models
{
    public class Quotation
    {
        public int Id { get; set; }
        [Required]
        public string SupplierId { get; set; } = string.Empty;
        [Required]
        public string Product { get; set; } = string.Empty;
      
        public string? CostPerUnit { get; set; } = string.Empty;
        public Supplier Supplier { get; set; }

    }
}
