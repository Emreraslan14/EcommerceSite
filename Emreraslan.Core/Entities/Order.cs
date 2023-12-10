using Emreraslan.Core.Entities.BaseEntity;

namespace Emreraslan.Core.Entities
{
    public class Order:IEntity
    {
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public int Discount { get; set; }
        
        public int VendorId { get; set; }
        public string UserId { get; set; }

        public List<ProductOrder> ProductOrders { get; set; }
        public Vendor Vendor { get; set; }
        public User User { get; set; }
    }
}
