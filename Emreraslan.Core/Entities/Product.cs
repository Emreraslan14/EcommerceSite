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
        public decimal Discount { get; set; }
        public string Brand { get; set; }
        public string PhotoPath { get; set; }


        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public Vendor Vendor { get; set; }
        public int VendorId { get; set; }
        public List<ProductOrder> ProductOrders { get; set; }

    }
}
