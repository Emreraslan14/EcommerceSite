﻿using Microsoft.AspNetCore.Mvc;

namespace Emreraslan.Web.Areas.VendorPanel.Controllers
{
    [Area("VendorPanel")]
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
