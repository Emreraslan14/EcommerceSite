using Emreraslan.Core.Entities;
using Emreraslan.Services.Abstract;
using Emreraslan.Web.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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


        [HttpPost]
        public async Task<JsonResult> Index(Product prod)
        {
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var userName = _contextAccessor.HttpContext.User.Identity.Name;

                var user = await _userManager.FindByNameAsync(userName);
                
                if (user != null)
                {
                    var vendor = _vendorService.Get(x => x.UserId == user.Id);
                    var category = _categoryService.Get(x => x.Name == vendor.Category);
                    prod.VendorId = vendor.Id;
                    prod.CategoryId = category.Id;

                    var result = await Task.FromResult(_productService.Insert(prod));

                    if(result == 1)
                        return Json( new {isOk = true , message = "Ürün kaydı başarıyla gerçekleşti."});

                    return Json(new { isOk = false, message = "Ürünü veri tabanına eklerken bir hatayla karşılaşıldı." });
                }

                return Json(new {isOk = false , message = "Kayıtlı kullanıcı bulunamadı."});
            }

            return Json(new { isOk = false, message = "Lütfen giriş yapınız." });
        }

        [HttpPost]
        public JsonResult UploadPhoto()
        {
            IFormFileCollection files = Request.Form.Files;
            
            if(files.Count > 0)
            {
                var fileName = files[0].FileName;
                var contentType = files[0].ContentType;

                if (!contentType.StartsWith("image/"))
                    return Json(new { isOk = false, message = "Lütfen Resim Dosyası Seçiniz..." });

                var randomFileName = ValueGenerater.FileNameGenerater(Path.GetExtension(fileName));

                string uploadPath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot/VendorPanelContent/images/products" , randomFileName);

                using(var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    files[0].CopyTo(stream);
                }

                return Json(new {isOk = true , path = "/VendorPanelContent/images/products/" + randomFileName });
            }
            else
            {
                return Json(new {isOk = false , message = "Lütfen Resim Dosyası Seçiniz..."});
            }
        }
    }
}
