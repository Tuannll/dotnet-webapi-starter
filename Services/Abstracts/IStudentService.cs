using WebAPIDemo.Dtos;
using WebAPIDemo.Dtos.Students;
using WebAPIDemo.Models;

namespace WebAPIDemo.Services.Abstracts;

public interface IStudentService
{
    Task<PagingDto<Student>> GetAllAsync(FilterStudentDto filter);
    Task<Student> GetByIdAsync(int id);
    Task<Student> CreateAsync(CreateStudentDto dto);
    Task<Student> UpdateAsync(int id, UpdateStudentDto dto);
    Task DeleteAsync(int id);
}
