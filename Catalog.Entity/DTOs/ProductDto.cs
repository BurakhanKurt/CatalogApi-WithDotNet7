
using System.ComponentModel.DataAnnotations;

namespace Catalog.Entity.DTOs
{
    public record ProductDto : ProductCreateDto
    {
        public int Id { get; init; }

    }
}
