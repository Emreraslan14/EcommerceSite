using Emreraslan.Core.Entities;
using Emreraslan.DataAccess.Repos.Abstract;
using Emreraslan.Services.Abstract;

namespace Emreraslan.Services.Concrete
{
    public class VendorService : GenericService<Vendor>, IVendorService
    {
        public VendorService(IGenericRepo<Vendor> repo) : base(repo)
        {
        }
    }
}
