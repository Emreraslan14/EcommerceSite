using Emreraslan.Core.Entities;
using Emreraslan.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Emreraslan.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    public class BeVendorController : Controller
    {
        private readonly IVendorService _vendorService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly ICategoryService _categoryService;
        public BeVendorController(IVendorService vendorService, UserManager<User> userManager,IHttpContextAccessor contextAccessor,ICategoryService categoryService)
        {
            _vendorService = vendorService;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var categories = _categoryService.GetAll();

            return View(categories);
        }

        [HttpPost]
        public async Task<JsonResult> Index(Vendor vendor)
        {
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                string name = _contextAccessor.HttpContext.User.Identity.Name;

                var user = await _userManager.FindByNameAsync(name);

                if (user == null)
                {
                    return Json(new { isOk = false, message = "Kullanıcı bulunamadı." });
                }

                vendor.UserId = user.Id;

                int result = await Task.FromResult(_vendorService.Insert(vendor));

                if (result == 1)
                    return Json(new { isOk = true, message = "Kayıt başarılı." });
                else
                    return Json(new { isOk = false, message = "Kayıt başarısız!!!" });
            }

            return Json(new { isOk = false, message = "Kayıt başarısız!!!" });
        }
    }

    /*
            int result = await Task.FromResult(_vendorService.Insert(vendor));
            if (result == 1)
                return Json(new { isOk = true, message = "Kayıt başarılı." });
            else
                return Json(new { isOk = false, message = "Kayıt başarısız!!!" });
     */
}
