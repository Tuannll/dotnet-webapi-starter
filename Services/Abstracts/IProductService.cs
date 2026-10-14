using WebAPIDemo.Dtos;
using WebAPIDemo.Dtos.Products;
using WebAPIDemo.Models;

namespace WebAPIDemo.Services.Abstracts;

public interface IProductService
{
    Task<PagingDto<Product>> GetAllAsync(FilterProductDto filter);
    Task<Product> GetByIdAsync(int id);
    Task<Product> CreateAsync(CreateProductDto dto);
    Task<Product> UpdateAsync(int id, UpdateProductDto dto);
    Task DeleteAsync(int id);
}
