using WebAPIDemo.Models;

namespace WebAPIDemo.Infrastructures;

public interface IApplicationDbContext
{
    // Products
    Task<List<Product>> GetProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
    Task<Product> AddProductAsync(Product product);
    Task<Product?> UpdateProductAsync(int id, Action<Product> updateAction);
    Task<bool> DeleteProductAsync(int id);

    // Students
    Task<List<Student>> GetStudentsAsync();
    Task<Student?> GetStudentByIdAsync(int id);
    Task<Student> AddStudentAsync(Student student);
    Task<Student?> UpdateStudentAsync(int id, Action<Student> updateAction);
    Task<bool> DeleteStudentAsync(int id);
}
