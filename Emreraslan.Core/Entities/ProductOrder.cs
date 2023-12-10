namespace Emreraslan.Core.Entities
{
    public class ProductOrder
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public Order Order{ get; set; }
        public int OrderId { get; set; }
    }
}
