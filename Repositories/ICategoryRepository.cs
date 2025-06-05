// ICategoryRepository.cs
using System.Collections.Generic;

namespace WebsiteBanHang.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories();
        Category? GetById(int id);
        void Add(Category category);
        void Update(Category category); // 👈 Thêm dòng này
        void Delete(int id);
    }
}
