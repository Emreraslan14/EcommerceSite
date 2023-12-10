using Emreraslan.Core.Entities;
using Emreraslan.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Emreraslan.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    public class BeVendorController : Controller
    {
        private readonly IVendorService _vendorService;

        public BeVendorController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Index(Vendor vendor)
        {
            int result = await Task.FromResult(_vendorService.Insert(vendor));
            if (result == 1)
                return Json(new { isOk = true, message = "Kayıt başarılı." });
            else
                return Json(new { isOk = false, message = "Kayıt başarısız!!!" });
        }
    }
}
