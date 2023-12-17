using Emreraslan.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emreraslan.DataAccess.Configuration.Mappings
{
    public class VendorConf : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> b)
        {
            b.HasKey(v => v.Id);
            b.Property(x => x.Id).UseIdentityColumn(1,1);

            b.HasMany(v => v.Products).WithOne(p => p.Vendor).HasForeignKey(p => p.VendorId);     
        }
    }
}
