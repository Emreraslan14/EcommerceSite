using Emreraslan.Core.Entities;
using Emreraslan.DataAccess.Contexts.EfCoreApp;
using Emreraslan.DataAccess.Repos.Abstract;

namespace Emreraslan.DataAccess.Repos.Concrete
{
    public class CategoryRepo : GenericRepo<Category>, ICategoryRepo
    {
        public CategoryRepo(AppDbContext ctx) : base(ctx)
        {
        }
    }
}
