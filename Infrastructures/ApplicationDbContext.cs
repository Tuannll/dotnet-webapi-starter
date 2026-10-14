using System.Collections.Concurrent;
using System.Threading;
using WebAPIDemo.Models;

namespace WebAPIDemo.Infrastructures;

public class ApplicationDbContext : IApplicationDbContext
{
    private readonly ConcurrentDictionary<int, Product> _products = new();
    private readonly ConcurrentDictionary<int, Student> _students = new();
    private int _lastProductId;
    private int _lastStudentId;

    public ApplicationDbContext()
    {
        var seedProducts = new[]
        {
            new Product { Id = 1, Name = "Banh mi", Price = 15000m },
            new Product { Id = 2, Name = "Sua tuoi", Price = 12000m },
            new Product { Id = 3, Name = "Ca phe", Price = 20000m }
        };
        foreach (var p in seedProducts)
        {
            _products[p.Id] = p;
        }
        _lastProductId = seedProducts.Max(p => p.Id);

        var seedStudents = new[]
        {
            new Student { Id = 1, Name = "Nguyen Van A", Age = 20 },
            new Student { Id = 2, Name = "Tran Thi B", Age = 21 },
            new Student { Id = 3, Name = "Le Van C", Age = 22 }
        };
        foreach (var s in seedStudents)
        {
            _students[s.Id] = s;
        }
        _lastStudentId = seedStudents.Max(s => s.Id);
    }

    public Task<List<Product>> GetProductsAsync()
    {
        return Task.FromResult(_products.Values.OrderBy(p => p.Id).ToList());
    }

    public Task<Product?> GetProductByIdAsync(int id)
    {
        _products.TryGetValue(id, out var product);
        return Task.FromResult(product);
    }

    public Task<Product> AddProductAsync(Product product)
    {
        product.Id = Interlocked.Increment(ref _lastProductId);
        _products[product.Id] = product;
        return Task.FromResult(product);
    }

    public Task<Product?> UpdateProductAsync(int id, Action<Product> updateAction)
    {
        if (_products.TryGetValue(id, out var product))
        {
            lock (product)
            {
                updateAction(product);
            }
            return Task.FromResult<Product?>(product);
        }
        return Task.FromResult<Product?>(null);
    }

    public Task<bool> DeleteProductAsync(int id)
    {
        return Task.FromResult(_products.TryRemove(id, out _));
    }

    public Task<List<Student>> GetStudentsAsync()
    {
        return Task.FromResult(_students.Values.OrderBy(s => s.Id).ToList());
    }

    public Task<Student?> GetStudentByIdAsync(int id)
    {
        _students.TryGetValue(id, out var student);
        return Task.FromResult(student);
    }

    public Task<Student> AddStudentAsync(Student student)
    {
        student.Id = Interlocked.Increment(ref _lastStudentId);
        _students[student.Id] = student;
        return Task.FromResult(student);
    }

    public Task<Student?> UpdateStudentAsync(int id, Action<Student> updateAction)
    {
        if (_students.TryGetValue(id, out var student))
        {
            lock (student)
            {
                updateAction(student);
            }
            return Task.FromResult<Student?>(student);
        }
        return Task.FromResult<Student?>(null);
    }

    public Task<bool> DeleteStudentAsync(int id)
    {
        return Task.FromResult(_students.TryRemove(id, out _));
    }
}
