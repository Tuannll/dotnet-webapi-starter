using Microsoft.AspNetCore.Mvc;

namespace WebAPIDemo.Dtos.Products;

public class FilterProductDto : FilterDto
{
    [FromQuery(Name = "minPrice")]
    public decimal? MinPrice { get; set; }

    [FromQuery(Name = "maxPrice")]
    public decimal? MaxPrice { get; set; }
}
