using Emreraslan.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emreraslan.DataAccess.Configuration.Mappings
{
    public class ProductOrderConf : IEntityTypeConfiguration<ProductOrder>
    {
        public void Configure(EntityTypeBuilder<ProductOrder> b)
        {
            b.HasKey(x => new {x.OrderId , x.ProductId});

            b.HasOne(x => x.Product).WithMany(x => x.ProductOrders).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(x => x.Order).WithMany(x => x.ProductOrders).HasForeignKey(x => x.OrderId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
