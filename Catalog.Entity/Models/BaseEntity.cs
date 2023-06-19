
namespace Catalog.Entity.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}