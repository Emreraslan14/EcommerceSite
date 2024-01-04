using Emreraslan.Core.Entities;
using Emreraslan.Services.Abstract;
using Emreraslan.Web.Extensions;
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

            List<Product> selectedProducts = HttpContext.Session.GetList<Product>("SelectedProducts");
            List<Product> wishProducts = HttpContext.Session.GetList<Product>("WishListProducts");


            ViewData["SelectedProduct"] = selectedProducts;
            ViewData["WishListProduct"] = wishProducts;

            List<Product> products = _productService.GetAll();

			return View(products);
		}

		public IActionResult ProductSingle(int id)
		{
			var prod = _productService.GetById(id);

			return View(prod);
		}

		public IActionResult AddToCart(int id)
		{
            List<Product> prods = HttpContext.Session.GetList<Product>("SelectedProducts") ?? new List<Product>();

            var product = _productService.GetById(id);

			prods.Add(product);

            HttpContext.Session.SetList("SelectedProducts", prods);

            return RedirectToAction("Index");
        }

        public IActionResult AddToWishList(int id)
        {
            List<Product> wishProds = HttpContext.Session.GetList<Product>("WishListProducts") ?? new List<Product>();

            var product = _productService.GetById(id);

            wishProds.Add(product);

            HttpContext.Session.SetList("WishListProducts", wishProds);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromWishlist(int id)
        {
            List<Product> products = HttpContext.Session.GetList<Product>("WishListProducts");

            int index = products.FindIndex(x => x.Id == id);

            products.RemoveAt(index);

            HttpContext.Session.SetList("WishListProducts", products);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int id)
        {
            List<Product> products = HttpContext.Session.GetList<Product>("SelectedProducts");

            int index = products.FindIndex(x => x.Id == id);

            products.RemoveAt(index);

            HttpContext.Session.SetList("SelectedProducts", products);

            return RedirectToAction("Index");
        }
    }
}
