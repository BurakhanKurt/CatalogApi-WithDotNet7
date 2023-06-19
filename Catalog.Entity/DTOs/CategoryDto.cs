
namespace Catalog.Entity.DTOs
{
    public record CategoryDto : CategoryCreateDto
    {
        public int Id { get; init; }
    }
}
