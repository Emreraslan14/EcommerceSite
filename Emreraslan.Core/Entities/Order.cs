using Emreraslan.Core.Entities.BaseEntity;

namespace Emreraslan.Core.Entities
{
    public class Order:IEntity
    {
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public int Discount { get; set; }
    }
}
