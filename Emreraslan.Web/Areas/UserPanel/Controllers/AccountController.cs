using Azure.Identity;
using Emreraslan.Core.Entities;
using Emreraslan.Services.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Emreraslan.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly UserService _userService;

        public AccountController(IHttpContextAccessor contextAccessor, UserManager<User> userManager, UserService userService)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var name = _contextAccessor.HttpContext.User.Identity.Name;

                var user = await _userManager.FindByNameAsync(name);

                return View(user);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProfile()
        {
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var name = _contextAccessor.HttpContext.User.Identity.Name;

                var user = await _userManager.FindByNameAsync(name);

                return View(user);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<JsonResult> UpdateProfile(User user)
        {
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var name = _contextAccessor.HttpContext.User.Identity.Name;

                var existedUser = await _userManager.FindByNameAsync(name);
                
                if (existedUser != null)
                {
                    var result = await _userService.UpdateUser(user, existedUser);
                    if (result)
                    {
                        return Json(new {isOk=true , message="Profiliniz başarıyla güncellendi."});
                    }
                    else
                    {
                        return Json(new { isOk = false, message = "Profil güncelleme başarısız!" });
                    }
                }                
            }
            return Json(new { isOk = false, message = "Lütfen Giriş Yapınız." });
        }
    }
}
