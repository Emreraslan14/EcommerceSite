using Emreraslan.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emreraslan.DataAccess.Configuration.Mappings
{
    public class CategoryConf : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> b)
        {
            b.HasKey(c => c.Id);
            b.Property(x => x.Id).UseIdentityColumn(1,1);

            b.HasMany(c => c.Products).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId);
        }
    }
}
