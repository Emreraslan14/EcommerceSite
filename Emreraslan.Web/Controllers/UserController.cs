using Emreraslan.Core.Dtos;
using Emreraslan.Services.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Emreraslan.Web.Controllers
{
	public class UserController : Controller
	{
		private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

		[HttpGet]
        public IActionResult SignUp()
		{
			return View();
		}

        [HttpPost]
        public async Task<JsonResult> SignUp(UserSignUpDto user)
        {
            var result = await _userService.SingUpUser(user);
            return Json(new {isOk=result.Item2,message=result.Item1});
        }
    }
}
