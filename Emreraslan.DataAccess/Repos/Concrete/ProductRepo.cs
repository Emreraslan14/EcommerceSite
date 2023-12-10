using Emreraslan.Core.Entities;
using Emreraslan.DataAccess.Contexts.EfCoreApp;
using Emreraslan.DataAccess.Repos.Abstract;

namespace Emreraslan.DataAccess.Repos.Concrete
{
    public class ProductRepo : GenericRepo<Product>, IProductRepo
    {
        public ProductRepo(AppDbContext ctx) : base(ctx)
        {
        }
    }
}
