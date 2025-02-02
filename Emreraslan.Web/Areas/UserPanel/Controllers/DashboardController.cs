﻿using Emreraslan.Services.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Emreraslan.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    public class DashboardController : Controller
    {
        private readonly UserService _userService;

        public DashboardController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutUser();

            return RedirectToAction("Index","Home", new {area = ""});
        }
    }
}
