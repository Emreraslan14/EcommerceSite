using Emreraslan.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emreraslan.DataAccess.Configuration.Mappings
{
    public class UserClaimConf : IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> b)
        {
            // Primary key
            b.HasKey(uc => uc.Id);
            b.Property(x => x.Id).ValueGeneratedOnAdd();

            // Maps to the AspNetUserClaims table
            b.ToTable("UserClaims");
        }
    }
}
