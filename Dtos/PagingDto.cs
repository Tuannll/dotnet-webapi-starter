using System;

namespace WebAPIDemo.Dtos;

public class PagingDto<T>
{
    public IEnumerable<T> Items { get; set; } = new List<T>();
    public int TotalItems { get; set; }
}
