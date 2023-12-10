using Emreraslan.Core.Entities;
using Emreraslan.DataAccess.Contexts.EfCoreApp;
using Emreraslan.DataAccess.Repos.Abstract;

namespace Emreraslan.DataAccess.Repos.Concrete
{
    public class OrderRepo : GenericRepo<Order>, IOrderRepo
    {
        public OrderRepo(AppDbContext ctx) : base(ctx)
        {
        }
    }
}
