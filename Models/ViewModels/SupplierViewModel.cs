using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SupplierWebApp.Models.ViewModels
{
    public class SupplierViewModel
    {
        public Supplier Supplier { get; set; }
        public IEnumerable<SelectListItem> CountryList { get; set; }
    }
}