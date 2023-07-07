
using System.ComponentModel.DataAnnotations;

namespace Catalog.Entity.DTOs
{
    public record CategoryDto : CategoryCreateDto
    {
        [Required(ErrorMessage = "Category Id is required.")]
        public int Id { get; init; }
    }
}
