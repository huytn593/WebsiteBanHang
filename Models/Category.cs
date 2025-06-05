﻿using System.ComponentModel.DataAnnotations;
using WebsiteBanHang.Models;

public class Category
{
    public int Id { get; set; }

    [Required, StringLength(50)]
    public string Name { get; set; } = string.Empty;
    public List<Product>? Products { get; set; }
}
