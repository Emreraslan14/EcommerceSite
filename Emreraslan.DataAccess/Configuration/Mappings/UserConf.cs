using Emreraslan.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emreraslan.DataAccess.Configuration.Mappings
{
    public class UserConf : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> b)
        {
            b.HasKey(u => u.Id);
            b.Property(x => x.Id).ValueGeneratedOnAdd();

            b.ToTable("users");

            b.HasMany<UserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();
            b.HasMany<UserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();
            b.HasMany<UserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

            b.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");
            b.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();

            b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            b.Property(u => u.UserName).HasMaxLength(256);
            b.Property(u => u.NormalizedUserName).HasMaxLength(256);
            b.Property(u => u.Email).HasMaxLength(256);
            b.Property(u => u.NormalizedEmail).HasMaxLength(256);
        }
    }
}
