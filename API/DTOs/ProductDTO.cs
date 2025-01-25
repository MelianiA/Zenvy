using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class ProductDTO
{
    [Range(1, double.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
    public decimal Price { get; set; }
    [Required]
    public string PictureUrl { get; set; } = string.Empty;
    [Required]
    public string Type { get; set; } = string.Empty;
    [Required]
    public string Brand { get; set; } = string.Empty;
    [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
    public int QuantityInStock { get; set; }
    public string Name { get; set; } = string.Empty;
    [Required]
    public string ArabicName { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
    [Required]
    public string ArabicDescription { get; set; } = string.Empty;
}
