namespace Emreraslan.Web.Models
{
    public class ProductDto
    {
        public IFormFile ProductPhoto { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public string Colour { get; set; }
        public decimal Discount { get; set; }
        public string Brand { get; set; }
    }
}
