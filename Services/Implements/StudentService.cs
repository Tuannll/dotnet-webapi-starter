using System.Net;
using WebAPIDemo.Dtos;
using WebAPIDemo.Dtos.Students;
using WebAPIDemo.Exceptions;
using WebAPIDemo.Infrastructures;
using WebAPIDemo.Models;
using WebAPIDemo.Services.Abstracts;

namespace WebAPIDemo.Services.Implements;

public class StudentService : IStudentService
{
    private readonly IApplicationDbContext _dbContext;

    public StudentService(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Student> CreateAsync(CreateStudentDto dto)
    {
        var student = new Student
        {
            Name = dto.Name.Trim(),
            Age = dto.Age
        };
        return await _dbContext.AddStudentAsync(student);
    }

    public async Task DeleteAsync(int id)
    {
        var deleted = await _dbContext.DeleteStudentAsync(id);
        if (!deleted)
        {
            throw new UserFriendlyException(
                ErrorCodes.StudentNotFound,
                $"Student with id {id} not found.",
                HttpStatusCode.NotFound
            );
        }
    }

    public async Task<PagingDto<Student>> GetAllAsync(FilterStudentDto filter)
    {
        var students = await _dbContext.GetStudentsAsync();
        var query = students.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Keyword))
        {
            var keyword = filter.Keyword.Trim();
            query = query.Where(s => s.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }

        if (filter.MinAge.HasValue)
        {
            query = query.Where(s => s.Age >= filter.MinAge.Value);
        }

        if (filter.MaxAge.HasValue)
        {
            query = query.Where(s => s.Age <= filter.MaxAge.Value);
        }

        var totalItems = query.Count();
        var items = query.Skip(filter.GetSkipCount())
            .Take(filter.PageSize)
            .ToList();

        return new PagingDto<Student>
        {
            Items = items,
            TotalItems = totalItems
        };
    }

    public async Task<Student> GetByIdAsync(int id)
    {
        var student = await _dbContext.GetStudentByIdAsync(id);
        if (student == null)
        {
            throw new UserFriendlyException(
                ErrorCodes.StudentNotFound,
                $"Student with id {id} not found.",
                HttpStatusCode.NotFound
            );
        }
        return student;
    }

    public async Task<Student> UpdateAsync(int id, UpdateStudentDto dto)
    {
        var updated = await _dbContext.UpdateStudentAsync(id, s =>
        {
            s.Name = dto.Name.Trim();
            s.Age = dto.Age;
        });

        if (updated == null)
        {
            throw new UserFriendlyException(
                ErrorCodes.StudentNotFound,
                $"Student with id {id} not found.",
                HttpStatusCode.NotFound
            );
        }

        return updated;
    }
}
