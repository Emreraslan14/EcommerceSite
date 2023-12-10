using Emreraslan.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emreraslan.DataAccess.Configuration.Mappings
{
    public class OrderConf : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> b)
        {
            b.HasKey(o => o.Id);
            b.Property(x => x.Id).UseIdentityColumn(1,1);
            
            b.HasOne(x => x.Vendor).WithMany(b => b.Orders).HasForeignKey(x => x.VendorId);
            b.HasOne(x => x.User).WithMany(b => b.Orders).HasForeignKey(x => x.UserId);
        }
    }
}
