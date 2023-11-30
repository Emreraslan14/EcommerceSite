using Emreraslan.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emreraslan.DataAccess.Configuration.Mappings
{
    public class UserLoginConf : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> b)
        {
            b.HasKey(ul => new { ul.LoginProvider, ul.ProviderKey });
            
            // Limit the size of the composite key columns due to common DB restrictions
            b.Property(ul => ul.LoginProvider).HasMaxLength(128);
            b.Property(ul => ul.ProviderKey).HasMaxLength(128);

            // Maps to the AspNetUserLogins table
            b.ToTable("UserLogins");
        }
    }
}
