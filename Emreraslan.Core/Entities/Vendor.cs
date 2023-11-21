using Emreraslan.Core.Entities.BaseEntity;

namespace Emreraslan.Core.Entities
{
    public class Vendor : IEntity
    {
        public bool ApplicationStatus { get; set; }
        public bool AccountStatus { get; set; }
    }
}
