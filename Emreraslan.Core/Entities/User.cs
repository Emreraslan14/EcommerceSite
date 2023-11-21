using Microsoft.AspNetCore.Identity;

namespace Emreraslan.Core.Entities
{
	public class User : IdentityUser<string>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
    }
}
