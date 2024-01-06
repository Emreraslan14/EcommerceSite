using Emreraslan.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emreraslan.Core.ViewModels
{
    public class VendorUserOrderProdVm
    {
        public Order Order { get; set; }
        public Vendor Vendor { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }
    }
}
