using Emreraslan.Core.Entities;
using Emreraslan.Services.Abstract;
using Emreraslan.Web.Helpers;
using Emreraslan.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Emreraslan.Web.Areas.VendorPanel.Controllers
{
    [Area("VendorPanel")]
    public class AddProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IVendorService _vendorService;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AddProductController(IProductService productService, UserManager<User> userManager, IHttpContextAccessor contextAccessor, IVendorService vendorService, ICategoryService categoryService, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _vendorService = vendorService;
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        //[HttpPost]
        //public async Task<JsonResult> Index(Product prod)
        //{
        //    if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
        //    {
        //        var userName = _contextAccessor.HttpContext.User.Identity.Name;

        //        var user = await _userManager.FindByNameAsync(userName);

        //        if (user != null)
        //        {
        //            var vendor = _vendorService.Get(x => x.UserId == user.Id);
        //            var category = _categoryService.Get(x => x.Name == vendor.Category);
        //            prod.VendorId = vendor.Id;
        //            prod.CategoryId = category.Id;

        //            var result = await Task.FromResult(_productService.Insert(prod));

        //            if (result == 1)
        //                return Json(new { isOk = true, message = "Ürün kaydı başarıyla gerçekleşti." });

        //            return Json(new { isOk = false, message = "Ürünü veri tabanına eklerken bir hatayla karşılaşıldı." });
        //        }

        //        return Json(new { isOk = false, message = "Kayıtlı kullanıcı bulunamadı." });
        //    }

        //    return Json(new { isOk = false, message = "Lütfen giriş yapınız." });
        //}

        [HttpPost]
        public async Task<IActionResult> UploadPhoto(ProductDto dto)
        {


            var randomFileName = ValueGenerater.FileNameGenerater(Path.GetExtension(dto.ProductPhoto.FileName));
            string uploadPath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", "VendorPanelContent", "images", "products", randomFileName);
            using (FileStream stream = new FileStream(uploadPath, FileMode.Create))
            {
                await dto.ProductPhoto.CopyToAsync(stream);
            }
            var userName = _contextAccessor.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var vendor = _vendorService.Get(x => x.UserId == user.Id);
                var category = _categoryService.Get(x => x.Name == vendor.Category);
                var vendorid = vendor.Id;
                var categoryid = category.Id;
                _productService.Insert(new Product
                {
                    Brand = dto.Brand,
                    Colour = dto.Colour,
                    ChangedDate = DateTime.Now,
                    Description = dto.Description,
                    IsActive = true,
                    IsDeleted = false,
                    PhotoPath = uploadPath,
                    Price = dto.Price,
                    Stock = dto.Stock,
                    Discount = dto.Discount,
                    Name = dto.Name,
                    CategoryId = categoryid,
                    VendorId = vendorid,

                });
            }






            return RedirectToAction("Index", "AddProduct", new { area = "VendorPanel" });
        }
    }
}
