using Emreraslan.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Emreraslan.Core.ViewModels
{
    public class UserProductOrderVm
    {
        public User User { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
        
    }
}
