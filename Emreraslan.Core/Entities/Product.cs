using Emreraslan.Core.Entities.BaseEntity;

namespace Emreraslan.Core.Entities
{
    public class Product:IEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public string Colour { get; set; }
        public decimal Size { get; set; }
        public string Brand { get; set; }
    }
}
