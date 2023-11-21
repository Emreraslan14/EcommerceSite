using Emreraslan.Core.Dtos;
using Emreraslan.Core.Entities;
using Microsoft.AspNetCore.Identity;

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
			return (" ", true);
		}

		public async Task<(string, bool)> SingUpUser(UserSignUpDto userSingUpDto)
		{
			if(userSingUpDto.Password != userSingUpDto.RepeatPassword)
			{
				return ("Şifreler birbiriyle uyuşmuyor.", false);
			}

			var user1 = await _userManager.FindByEmailAsync(userSingUpDto.Email);
			if(user1 != null)
			{
				return ("Böyle bir email zaten kullanılmakta.", false);
			}

			var user2 = await _userManager.FindByNameAsync(userSingUpDto.UserName);
			if(user2 != null)
			{
				return ("Böyle bir kullanıcı adı zaten kullanılıyor.", false);
			}

			var newUser = new User()
			{
				Name = userSingUpDto.Name,
				Email = userSingUpDto.Email,
				UserName = userSingUpDto.UserName,
				Address = userSingUpDto.Address,
				PhoneNumber = userSingUpDto.PhoneNumber,
				Surname = userSingUpDto.Surname				
			};

			var result = await _userManager.CreateAsync(newUser, userSingUpDto.Password);
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
	}
}
