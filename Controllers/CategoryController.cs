using Microsoft.AspNetCore.Mvc;
using WebsiteBanHang.Models;
using WebsiteBanHang.Repositories;

namespace WebsiteBanHang.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        // ✅ Danh sách Category
        public IActionResult Index()
        {
            var categories = _categoryRepo.GetAllCategories();
            return View(categories);
        }

        // ✅ Hiển thị chi tiết
        public IActionResult Details(int id)
        {
            var category = _categoryRepo.GetById(id);
            return category == null ? NotFound() : View(category);
        }

        // ✅ Thêm mới Category (GET)
        public IActionResult Add()
        {
            return View();
        }

        // ✅ Thêm mới Category (POST)
        [HttpPost]
        public IActionResult Add(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepo.Add(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // ✅ Cập nhật Category (GET)
        public IActionResult Update(int id)
        {
            var category = _categoryRepo.GetById(id);
            return category == null ? NotFound() : View(category);
        }

        // ✅ Cập nhật Category (POST)
        [HttpPost]
        public IActionResult Update(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepo.Update(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // ✅ Xoá Category (GET)
        public IActionResult Delete(int id)
        {
            var category = _categoryRepo.GetById(id);
            return category == null ? NotFound() : View(category);
        }

        // ✅ Xoá Category (POST)
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _categoryRepo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
