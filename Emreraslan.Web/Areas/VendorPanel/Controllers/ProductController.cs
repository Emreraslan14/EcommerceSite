using Emreraslan.Core.Entities;
using Emreraslan.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Emreraslan.Web.Areas.VendorPanel.Controllers
{
    [Area("VendorPanel")]
    public class ProductController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly IVendorService _vendorService;
        private readonly IProductService _productService;

        public ProductController(IHttpContextAccessor contextAccessor, UserManager<User> userManager, IVendorService vendorService, IProductService productService)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _vendorService = vendorService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var name = _contextAccessor.HttpContext.User.Identity.Name;

                var user = await _userManager.FindByNameAsync(name);

                var vendor = _vendorService.Get(x => x.UserId == user.Id);

                if (vendor == null)
                {
                    return NotFound();
                }

                List<Product> products = _productService.GetAll(x => x.VendorId ==  vendor.Id);

                return View(products);
            }

            return NotFound();
        }

        public IActionResult Delete(int id)
        {
            Product prod = _productService.GetById(id);
            _productService.Delete(prod);
            return RedirectToAction("Index", "Product", new { area = "VendorPanel" });
        }
    }
}
