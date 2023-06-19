
using System.ComponentModel.DataAnnotations;

namespace Catalog.Entity.DTOs
{
    public record CategoryCreateDto
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; init; }

        [StringLength(50, ErrorMessage = "Description cannot exceed 50 characters.")]
        public string Description { get; init; }
    }
}
