using Emreraslan.Core.Entities;
using Emreraslan.Services.Abstract;
using Emreraslan.Services.Concrete;
using Emreraslan.Web.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Emreraslan.Web.Controllers
{
    public class HomeController : Controller
	{
		private readonly IProductService _productService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IOrderService _orderService;        
        private readonly ICategoryService _categoryService;        
        private readonly UserManager<User> _userManager;
        private readonly ProductOrderService _productOrderService;

        public HomeController(IProductService productService , IHttpContextAccessor contextAccessor, IOrderService orderService, UserManager<User> userManager, ICategoryService categoryService, ProductOrderService productOrderService)
        {
            _productService = productService;
            _contextAccessor = contextAccessor;
            _orderService = orderService;
            _userManager = userManager;
            _categoryService = categoryService;
            _productOrderService = productOrderService;
        }

        public async Task<IActionResult> Index()
		{
			if (_contextAccessor.HttpContext.User.IsInRole("Admin"))
			{
				return RedirectToAction("Index", "Dashboard", new {area = "AdminPanel"});
			}

            List<Product> selectedProducts = HttpContext.Session.GetList<Product>("SelectedProducts");
            List<Product> wishProducts = HttpContext.Session.GetList<Product>("WishListProducts");
            List<Category> categories = _categoryService.GetAll();

            ViewData["SelectedProduct"] = selectedProducts;
            ViewData["WishListProduct"] = wishProducts;
            ViewData["Categories"] = categories;

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
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                List<Product> prods = HttpContext.Session.GetList<Product>("SelectedProducts") ?? new List<Product>();

                var product = _productService.GetById(id);

                prods.Add(product);

                HttpContext.Session.SetList("SelectedProducts", prods);

                return RedirectToAction("Index");
            }

            return NotFound();
        }

        public IActionResult AddToWishList(int id)
        {
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                List<Product> wishProds = HttpContext.Session.GetList<Product>("WishListProducts") ?? new List<Product>();

                var product = _productService.GetById(id);

                wishProds.Add(product);

                HttpContext.Session.SetList("WishListProducts", wishProds);

                return RedirectToAction("Index");
            }

            return NotFound();
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

        public async Task<IActionResult> CreateOrder()
        {
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                List<Product> productsForOrder = HttpContext.Session.GetList<Product>("SelectedProducts");

                var userName = _contextAccessor.HttpContext.User.Identity.Name;

                if (userName != null)
                {
                    var user = await _userManager.FindByNameAsync(userName);

                    if (user != null)
                    {
                        List<Product> productsToRemove = new List<Product>();

                        foreach (var product in productsForOrder)
                        {
                            Order order = new Order()
                            {
                                OrderDate = DateTime.Now,
                                Discount = (int)(product.Price * product.Discount / 100),
                                UserId = user.Id,
                                VendorId = product.VendorId,
                                Quantity = 1,
                                TotalPrice = product.Price - product.Discount
                            };
                            
                            int result = _orderService.Insert(order);

                            if(result == 1) 
                            {
                                ProductOrder prodOrd = new ProductOrder()
                                {
                                    ProductId = product.Id,
                                    OrderId = order.Id
                                };

                                _productOrderService.Insert(prodOrd);

                                productsToRemove.Add(product);                                
                            }                           
                        }

                        foreach (var productToRemove in productsToRemove)
                        {
                            productsForOrder.Remove(productToRemove);
                        }

                        HttpContext.Session.SetList("SelectedProducts", productsForOrder);

                        return RedirectToAction("Index");
                    }                    
                }                
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult ContactUs()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult ContactUs()
        //{
        //    return View();
        //}

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult PrivacyPolicy()
        {
            return View();
        }

        public IActionResult FrequentlyQuestions()
        {
            return View();
        }
    }
}
