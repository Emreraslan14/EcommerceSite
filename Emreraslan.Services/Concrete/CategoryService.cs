using Emreraslan.Core.Entities;
using Emreraslan.DataAccess.Repos.Abstract;
using Emreraslan.Services.Abstract;

namespace Emreraslan.Services.Concrete
{
    public class CategoryService : GenericService<Category>, ICategoryService
    {
        public CategoryService(IGenericRepo<Category> repo) : base(repo)
        {

        }
    }
}
