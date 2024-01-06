using Emreraslan.Core.Entities;
using Emreraslan.Core.ViewModels;
using Emreraslan.Services.Abstract;
using Emreraslan.Services.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Emreraslan.Web.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly UserManager<User> _userManager;
        private readonly IVendorService _vendorService;
        private readonly ProductOrderService _productOrderService;

        public OrderController(IOrderService orderService, IProductService productService, UserManager<User> userManager, IVendorService vendorService,ProductOrderService productOrderService)
        {
            _orderService = orderService;
            _productService = productService;
            _userManager = userManager;
            _vendorService = vendorService;
            _productOrderService = productOrderService;
        }

        public async Task<IActionResult> Index()
        {
            List<VendorUserOrderProdVm> vms = new List<VendorUserOrderProdVm>();

            List<Order> orders = _orderService.GetAll();

            foreach (Order order in orders)
            {
                VendorUserOrderProdVm vm = new VendorUserOrderProdVm();

                vm.Order = order;

                vm.User = await _userManager.FindByIdAsync(order.UserId);

                vm.Vendor = _vendorService.GetById(order.VendorId);

                var prdOrd = _productOrderService.GetByOrderId(order.Id);
                var prodId = prdOrd.ProductId;

                vm.Product = _productService.GetById(prodId);

                vms.Add(vm);
            }

            return View(vms);
        }
    }
}
