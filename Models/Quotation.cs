using System.ComponentModel.DataAnnotations;

namespace SupplierWebApp.Models
{
    public class Quotation
    {
        public int Id { get; set; }

        [Required]
        public string Product { get; set; }

        public decimal? CostPerUnit { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

    }
}
