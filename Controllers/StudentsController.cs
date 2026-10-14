using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Dtos;
using WebAPIDemo.Dtos.Students;
using WebAPIDemo.Models;
using WebAPIDemo.Services.Abstracts;

namespace WebAPIDemo.Controllers;

[ApiController]
[Route("api/students")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<ActionResult<PagingDto<Student>>> GetAll([FromQuery] FilterStudentDto filter)
    {
        var result = await _studentService.GetAllAsync(filter);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Student>> GetById(int id)
    {
        var student = await _studentService.GetByIdAsync(id);
        return Ok(student);
    }

    [HttpPost]
    public async Task<ActionResult<Student>> Create([FromBody] CreateStudentDto dto)
    {
        var created = await _studentService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Student>> Update(int id, [FromBody] UpdateStudentDto dto)
    {
        var updated = await _studentService.UpdateAsync(id, dto);
        return Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _studentService.DeleteAsync(id);
        return NoContent();
    }
}
