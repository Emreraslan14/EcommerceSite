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
			b.Property(v => v.AccountStatus)
				.IsRequired(false)
				.HasDefaultValue(true);			

			// Vendor configuration
			b.HasOne(x => x.User)
				.WithOne(x => x.Vendor)
				.HasForeignKey<Vendor>(x => x.UserId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

			// Products configuration
			b.HasMany(v => v.Products)
				.WithOne(p => p.Vendor)
				.HasForeignKey(p => p.VendorId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);
		}
    }
}
