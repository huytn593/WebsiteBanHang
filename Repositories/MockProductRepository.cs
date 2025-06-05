using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebsiteBanHang.Models;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Product> GetAll()
    {
        // Include nếu có các liên kết ảnh sản phẩm
        return _context.Products.Include(p => p.ProductImages).ToList();
    }

    public Product GetById(int id)
    {
        return _context.Products.Include(p => p.ProductImages).FirstOrDefault(p => p.Id == id);
    }

    public void Add(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges(); // Cần gọi mới thực sự lưu DB
    }

    public void Update(Product product)
    {
        _context.Products.Update(product);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var product = _context.Products.Find(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}
