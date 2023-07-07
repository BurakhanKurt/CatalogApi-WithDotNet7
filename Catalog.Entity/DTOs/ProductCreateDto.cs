
using System.ComponentModel.DataAnnotations;

namespace Catalog.Entity.DTOs
{
    public record ProductCreateDto
    {
        [Required(ErrorMessage = "Namesss is required.")]
        public string Name { get; init; }

        [Required(ErrorMessage = "CategoryId is required.")]
        public int CategoryId { get; init; }

        [Required(ErrorMessage = "Stock is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock must be a non-negative integer.")]
        public int Stock { get; init; }

        [Required(ErrorMessage = "Price is required.")]
        [Range( 0, 10000, ErrorMessage = "Maximum price should be less than 10000 and greater than 0")]
        public decimal Price { get; init; }

        [StringLength(50, ErrorMessage = "Description cannot exceed 50 characters.")]
        public string Description { get; init; }
    }
}
