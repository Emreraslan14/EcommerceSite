using Emreraslan.Core.Entities;
using Emreraslan.DataAccess.Repos.Abstract;
using Emreraslan.Services.Abstract;

namespace Emreraslan.Services.Concrete
{
    public class OrderService : GenericService<Order>, IOrderService
    {
        public OrderService(IGenericRepo<Order> repo) : base(repo)
        {
        }
    }
}
