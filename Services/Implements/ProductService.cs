using System.Net;
using WebAPIDemo.Dtos;
using WebAPIDemo.Dtos.Products;
using WebAPIDemo.Exceptions;
using WebAPIDemo.Infrastructures;
using WebAPIDemo.Models;
using WebAPIDemo.Services.Abstracts;

namespace WebAPIDemo.Services.Implements;

public class ProductService : IProductService
{
    private readonly IApplicationDbContext _dbContext;

    public ProductService(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product> CreateAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name.Trim(),
            Price = dto.Price
        };
        return await _dbContext.AddProductAsync(product);
    }

    public async Task DeleteAsync(int id)
    {
        var deleted = await _dbContext.DeleteProductAsync(id);
        if (!deleted)
        {
            throw new UserFriendlyException(
                ErrorCodes.ProductNotFound,
                $"Product with id {id} not found.",
                HttpStatusCode.NotFound
            );
        }
    }

    public async Task<PagingDto<Product>> GetAllAsync(FilterProductDto filter)
    {
        var products = await _dbContext.GetProductsAsync();
        var query = products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Keyword))
        {
            var keyword = filter.Keyword.Trim();
            query = query.Where(p => p.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }

        if (filter.MinPrice.HasValue)
        {
            query = query.Where(p => p.Price >= filter.MinPrice.Value);
        }

        if (filter.MaxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= filter.MaxPrice.Value);
        }

        var totalItems = query.Count();
        var items = query.Skip(filter.GetSkipCount())
            .Take(filter.PageSize)
            .ToList();

        return new PagingDto<Product>
        {
            Items = items,
            TotalItems = totalItems
        };
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        var product = await _dbContext.GetProductByIdAsync(id);
        if (product == null)
        {
            throw new UserFriendlyException(
                ErrorCodes.ProductNotFound,
                $"Product with id {id} not found.",
                HttpStatusCode.NotFound
            );
        }
        return product;
    }

    public async Task<Product> UpdateAsync(int id, UpdateProductDto dto)
    {
        var updated = await _dbContext.UpdateProductAsync(id, p =>
        {
            p.Name = dto.Name.Trim();
            p.Price = dto.Price;
        });

        if (updated == null)
        {
            throw new UserFriendlyException(
                ErrorCodes.ProductNotFound,
                $"Product with id {id} not found.",
                HttpStatusCode.NotFound
            );
        }

        return updated;
    }
}
