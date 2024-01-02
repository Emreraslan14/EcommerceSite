using Emreraslan.Core.Entities;
using Emreraslan.Services.Abstract;
using Emreraslan.Services.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Emreraslan.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly IProductService _productService;
        private readonly IHttpContextAccessor _contextAccessor;

        public HomeController(IProductService productService , IHttpContextAccessor contextAccessor)
        {
            _productService = productService;
            _contextAccessor = contextAccessor;
        }

        public async Task<IActionResult> Index()
		{
			if (_contextAccessor.HttpContext.User.IsInRole("Admin"))
			{
				return RedirectToAction("Index", "Dashboard", new {area = "AdminPanel"});
			}

			List<Product> products = _productService.GetAll();
			return View(products);
		}
	}
}
