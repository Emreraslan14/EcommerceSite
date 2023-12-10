using Emreraslan.Core.Entities.BaseEntity;

namespace Emreraslan.Core.Entities
{
    public class Vendor : IEntity
    {
        public bool ApplicationStatus { get; set; }
        public bool AccountStatus { get; set; }
        public string CompanyName { get; set; }
        public string EstablishedYear { get; set; }
        public int TotalEmployees { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Category { get; set; }


        public List<Product> Products { get; set; }
        public List<Order> Orders { get; set; } 
    }
}
