using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIDemo.Dtos;

public class FilterDto
{
    [Range(1, 100, ErrorMessage = "Page size must be greater than 0.")]
    [FromQuery(Name = "pageSize")]
    public int PageSize { get; set; } = 10;

    [Range(1, int.MaxValue, ErrorMessage = "Page index must be greater than 0.")]
    [FromQuery(Name = "pageIndex")]
    public int PageIndex { get; set; } = 1;

    [FromQuery(Name = "keyword")]
    public string? Keyword
    { 
        get => field;
        set => field = value?.Trim();
    }

    public int GetSkipCount()
    {
        return (PageIndex - 1) * PageSize;
    }
}
