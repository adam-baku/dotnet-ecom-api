using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;
using Web.ModelBindingValidation;

namespace Web.DTO
{
    public class NewProductDTO
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [Range(1, 10000)]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, 10000.00)]
        public double NetPrice { get; set; }

        [Required]
        [Range(Common.Price.Tax.Bound.MIN, Common.Price.Tax.Bound.MAX)]
        public double Tax { get; set; }

        [Required]
        [Currency]
        public string Currency { get; set; }
    }
}
