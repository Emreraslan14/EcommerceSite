using Emreraslan.Core.Entities;
using Emreraslan.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Emreraslan.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    public class OrderController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IOrderService _orderService;
        private readonly UserManager<User> _userManager;

        public OrderController(IOrderService orderService, UserManager<User> userManager,IHttpContextAccessor contextAccessor)
        {
            _orderService = orderService;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var userName = _contextAccessor.HttpContext.User.Identity.Name;
                var user = await _userManager.FindByNameAsync(userName);
                if (user != null) 
                {
                    List<Order> orders = _orderService.GetAll(x => x.UserId == user.Id);
                    return View(orders);
                }
            }
            return NotFound();
        }

        public IActionResult DeleteOrder(int id)
        {
            var orderToDelete = _orderService.GetById(id);
            _orderService.Delete(orderToDelete);

            return RedirectToAction("Index");
        }
    }
}
