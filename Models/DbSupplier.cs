using System.ComponentModel.DataAnnotations;

namespace SupplierWebApp.Models
{
    public class DbSupplier
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(2)]
        public string CountryCode { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
    }
}
