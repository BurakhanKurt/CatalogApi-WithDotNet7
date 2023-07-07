

using System.ComponentModel.DataAnnotations;

namespace Catalog.Entity.DTOs
{
    public record ProductDto : ProductCreateDto
    {
        [Required(ErrorMessage = "Product Id is required.")]
        public int Id { get; init; }

    }
}
