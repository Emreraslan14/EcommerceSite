using Emreraslan.Core.Entities;
using Emreraslan.DataAccess.Repos.Abstract;
using Emreraslan.Services.Abstract;

namespace Emreraslan.Services.Concrete
{
    public class ProductService : GenericService<Product>, IProductService
    {
        public ProductService(IGenericRepo<Product> repo) : base(repo)
        {

        }
    }
}
