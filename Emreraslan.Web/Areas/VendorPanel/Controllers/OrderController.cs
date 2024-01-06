using Emreraslan.Core.Entities;
using Emreraslan.Core.ViewModels;
using Emreraslan.Services.Abstract;
using Emreraslan.Services.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Emreraslan.Web.Areas.VendorPanel.Controllers
{
    [Area("VendorPanel")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly IVendorService _vendorService;
        private readonly IProductService _productService;
        private readonly ProductOrderService _prdOrdService;

        public OrderController(IOrderService orderService, IVendorService vendorService,IHttpContextAccessor contextAccessor,UserManager<User> userManager, IProductService productService, ProductOrderService prdOrdService)
        {
            _orderService = orderService;
            _vendorService = vendorService;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _productService = productService;
            _prdOrdService = prdOrdService;
        }

        public async Task<IActionResult> Index()
        {
            List<UserProductOrderVm> vms = new List<UserProductOrderVm>();

            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var userName = _contextAccessor.HttpContext.User.Identity.Name;
                var user = await _userManager.FindByNameAsync(userName);
                if(user != null)
                {
                    var vendor = _vendorService.Get(x => x.UserId == user.Id);

                    List<Order> orders = _orderService.GetAll(x => x.VendorId == vendor.Id);              
                    
                    foreach(var order in orders)
                    {
                        UserProductOrderVm vm = new UserProductOrderVm();

                        vm.Order = order; //Order

                        ProductOrder prdOrd = new ProductOrder();

                        prdOrd = _prdOrdService.GetByOrderId(order.Id);

                        var prodId = prdOrd.ProductId;

                        var product = _productService.GetById(prodId);

                        vm.Product = product; //Product

                        vm.User = await _userManager.FindByIdAsync(order.UserId);  //User

                        vms.Add(vm);
                    }

                }
            }

            return View(vms);
        }
    }
}
