using Emreraslan.Core.Entities;
using Emreraslan.DataAccess.Contexts.EfCoreApp;
using Emreraslan.DataAccess.Repos.Abstract;

namespace Emreraslan.DataAccess.Repos.Concrete
{
    public class VendorRepo : GenericRepo<Vendor>, IVendorRepo
    {
        public VendorRepo(AppDbContext ctx) : base(ctx)
        {
        }
    }
}
