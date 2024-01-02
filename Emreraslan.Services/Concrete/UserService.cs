using Emreraslan.Core.Dtos;
using Emreraslan.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Emreraslan.Services.Concrete
{
    public class UserService
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		public UserService(UserManager<User> userManager, SignInManager<User> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public async Task<(string, bool)> SignInUser(UserLoginDto userLoginDto)
		{
			var user = await _userManager.FindByNameAsync(userLoginDto.UserName);
			if (user == null)
			{
				return ("Kullanıcı adı veya şifre hatalı.", false);
			}

			var checkedPassword = await _userManager.CheckPasswordAsync(user, userLoginDto.Password);
			if (!checkedPassword)
			{
				return ("Kullanıcı adı veya şifre hatalı.", false);
			}

			await _signInManager.SignInAsync(user, userLoginDto.IsRemember);

			bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

			if (isAdmin)
			{
                return ("Admin", true);
            }

			return ("Kullanici", true);
		}

		public async Task<(string, bool)> SignUpUser(UserSignUpDto userSignUpDto)
		{
			if (userSignUpDto.Password != userSignUpDto.RepeatPassword)
			{
				return ("Şifreler birbiriyle uyuşmuyor.", false);
			}

			var user1 = await _userManager.FindByEmailAsync(userSignUpDto.Email);
			if(user1 != null)
			{
				return ("Böyle bir email zaten kullanılmakta.", false);
			}

			var user2 = await _userManager.FindByNameAsync(userSignUpDto.UserName);
			if(user2 != null)
			{
				return ("Böyle bir kullanıcı adı zaten kullanılıyor.", false);
			}

			var newUser = new User()
			{
				Name = userSignUpDto.Name,
				Email = userSignUpDto.Email,
				UserName = userSignUpDto.UserName,
				Address = userSignUpDto.Address,
				PhoneNumber = userSignUpDto.PhoneNumber,
				Surname = userSignUpDto.Surname				
			};

			var result = await _userManager.CreateAsync(newUser, userSignUpDto.Password);
			if (!result.Succeeded)
			{
				string message = string.Empty;
                foreach (var error in result.Errors)
                {
					message += error;
                }
				return (message,false);
            }

			return ("Hesabınız başarıyla oluşturuldu.", true);
		}

		public async Task SignOutUser()
		{
            await _signInManager.SignOutAsync();
        }

		public async Task<List<User>> GetAll()
		{
			List<User> users = await _userManager.Users.ToListAsync();
			return users;
		}

		public async Task<bool> DeleteUser(string id) 
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user != null)
			{
				var result = await _userManager.DeleteAsync(user);
				if (result.Succeeded)
				{
					return (true);
				}
				else
				{
					return (false);
				}
			}

			return (false);

		}
	}
}
