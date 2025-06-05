using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebsiteBanHang.Models;
using WebsiteBanHang.Repositories;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;

        public ProductController(IProductRepository productRepo, ICategoryRepository categoryRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
        }

        public IActionResult Index() => View(_productRepo.GetAll());

        public IActionResult Display(int id)
        {
            var p = _productRepo.GetById(id);
            return p == null ? NotFound() : View(p);
        }

        public IActionResult Add()
        {
            ViewBag.Categories = new SelectList(_categoryRepo.GetAllCategories(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product product, IFormFile imageUrl, List<IFormFile> imageUrls)
        {
            if (ModelState.IsValid)
            {
                if (imageUrl != null)
                    product.ImageUrl = await SaveImage(imageUrl);

            if (imageUrls != null && imageUrls.Any())
            {
                product.ProductImages = new List<ProductImage>();
                foreach (var img in imageUrls)
                {
                    var imagePath = await SaveImage(img);
                    product.ProductImages.Add(new ProductImage
                    {
                        Url = imagePath
                    });
                }
            }


            _productRepo.Add(product);
                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(_categoryRepo.GetAllCategories(), "Id", "Name");
            return View(product);
        }

        public IActionResult Update(int id)
        {
            var p = _productRepo.GetById(id);
            return p == null ? NotFound() : View(p);
        }

        [HttpPost]
        public IActionResult Update(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepo.Update(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public IActionResult Delete(int id)
        {
            var p = _productRepo.GetById(id);
            return p == null ? NotFound() : View(p);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _productRepo.Delete(id);
            return RedirectToAction("Index");
        }

        private async Task<string> SaveImage(IFormFile file)
        {
            var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            if (!Directory.Exists(imagesPath))
                Directory.CreateDirectory(imagesPath);

            var filePath = Path.Combine(imagesPath, file.FileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return "/images/" + file.FileName;
        }
    }
