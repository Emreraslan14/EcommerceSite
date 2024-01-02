using Emreraslan.Core.Entities;
using Emreraslan.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Emreraslan.Web.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            List<Category> categories = _categoryService.GetAll();
            return View(categories);
        }

        public JsonResult AddCategory(Category category) 
        { 
            if(category == null)
            {
                return Json(new {isOk= false , message = "Category post edilemedi."});
            }
            int result = _categoryService.Insert(category);
            if(result==1)
            {
                return Json(new {isOk=true, message="Category ekleme başarılı."});
            }
            return Json(new {isOk=false,message="Ekleme işlemi başarısız!"});
        }
    }
}
