namespace Emreraslan.Core.Dtos
{
	public class UserSignUpDto
	{
        public string Email { get; set; }
		public string UserName { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
    }
}
