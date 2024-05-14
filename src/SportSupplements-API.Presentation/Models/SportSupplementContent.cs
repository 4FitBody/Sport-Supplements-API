

using SportSupplements_API.Core.Models;

namespace SportSupplements_API.Presentation.Models;

public class SportSupplementContent
{
    public SportSupplement? SportSupplement { get; set; }
    public string? ImageFileName { get; set; }
    public byte[]? ImageFileContent { get; set; }

}
