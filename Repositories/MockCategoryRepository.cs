// CategoryRepository.cs
using System;
using System.Collections.Generic;
using System.Linq;
using WebsiteBanHang.Models;

namespace WebsiteBanHang.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAllCategories() => _context.Categories.ToList();

        public Category? GetById(int id) => _context.Categories.FirstOrDefault(c => c.Id == id);

        public void Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var cat = _context.Categories.Find(id);
            if (cat != null)
            {
                _context.Categories.Remove(cat);
                _context.SaveChanges();
            }
        }
    }
}
