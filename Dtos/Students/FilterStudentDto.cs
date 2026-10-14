using Microsoft.AspNetCore.Mvc;

namespace WebAPIDemo.Dtos.Students;

public class FilterStudentDto : FilterDto
{
    [FromQuery(Name = "minAge")]
    public int? MinAge { get; set; }

    [FromQuery(Name = "maxAge")]
    public int? MaxAge { get; set; }
}
