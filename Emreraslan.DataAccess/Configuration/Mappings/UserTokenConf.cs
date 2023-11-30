using Emreraslan.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emreraslan.DataAccess.Configuration.Mappings
{
    public class UserTokenConf : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> b)
        {
            b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });            
            // Limit the size of the composite key columns due to common DB restrictions
            b.Property(t => t.LoginProvider).HasMaxLength(256);
            b.Property(t => t.Name).HasMaxLength(256);

            // Maps to the AspNetUserTokens table
            b.ToTable("UserTokens");
        }
    }
}
