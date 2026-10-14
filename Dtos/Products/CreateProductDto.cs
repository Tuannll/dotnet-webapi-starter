using System.ComponentModel.DataAnnotations;

namespace WebAPIDemo.Dtos.Products;

public class CreateProductDto
{
    [Required(ErrorMessage = "Tên sản phẩm không được để trống.")]
    [StringLength(150, MinimumLength = 1, ErrorMessage = "Tên sản phẩm phải từ 1 đến 150 ký tự.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Giá sản phẩm không được để trống.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Giá sản phẩm phải lớn hơn 0.")]
    public decimal Price { get; set; }
}
