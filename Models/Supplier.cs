using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SupplierWebApp.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required, MaxLength(2)]
        public string CountryCode { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        public ICollection<Quotation> Quotations { get; set; }


    }
}
