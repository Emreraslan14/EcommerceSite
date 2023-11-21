using Microsoft.AspNetCore.Mvc;

namespace Emreraslan.Web.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
