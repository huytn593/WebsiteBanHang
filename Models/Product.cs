using System.ComponentModel.DataAnnotations;

namespace WebsiteBanHang.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Range(0.01, 10000.00)]
        public decimal Price { get; set; }

        public string Description { get; set; } = string.Empty;

        // Foreign Key
        public int? CategoryId { get; set; }  // ✅ Cho phép null

        public Category? Category { get; set; }

        public string? ImageUrl { get; set; } // ảnh đại diện

        public List<ProductImage>? ProductImages { get; set; } = new();
        public List<ProductVideo>? ProductVideos { get; set; } = new();
    }


    public class ProductImage
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;

        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }

    public class ProductVideo
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;

        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }


}
