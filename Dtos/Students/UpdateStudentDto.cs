using System.ComponentModel.DataAnnotations;

namespace WebAPIDemo.Dtos.Students;

public class UpdateStudentDto
{
    [Required(ErrorMessage = "Tên sinh viên không được để trống.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Tên sinh viên phải từ 2 đến 100 ký tự.")]
    public string Name { get; set; } = string.Empty;

    [Range(1, 120, ErrorMessage = "Tuổi sinh viên phải từ 1 đến 120.")]
    public int Age { get; set; }
}
