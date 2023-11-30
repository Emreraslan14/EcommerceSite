using Emreraslan.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emreraslan.DataAccess.Configuration.Mappings
{
    public class RoleConf : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> b)
        {
            b.Property(x => x.Id).ValueGeneratedOnAdd();
            b.ToTable("Roles");
        }
    }
}
