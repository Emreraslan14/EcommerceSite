using Microsoft.AspNetCore.Mvc;

namespace Emreraslan.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    public class DashboardController : Controller
    {
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
