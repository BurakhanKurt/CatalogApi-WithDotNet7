
namespace Catalog.Entity.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        //ForignKey
        public int CategoryId { get; set; }
        //Nav prop
        public Category? Category { get; set; }
    }
}
