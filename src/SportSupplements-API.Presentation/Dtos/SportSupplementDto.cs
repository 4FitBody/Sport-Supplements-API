

namespace SportSupplements_API.Presentation.Dtos;

public class SportSupplementDto
{
    public string? Name { get; set; }

    public string? ImageUrl { get; set; }

    public string? Description { get; set; }

    public string? ManufactureCountry { get; set; }

    public double? Quantity { get; set; }

    public bool IsApproved { get; set; }
}
