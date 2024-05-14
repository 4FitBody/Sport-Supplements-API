
using MediatR;
using SportSupplements_API.Core.Models;

namespace SportSupplements_API.Infrastructure.Commands;

public class CreateCommand : IRequest
{
    public SportSupplement? SportSupplement { get; set; }
    public CreateCommand(SportSupplement? sportSupplement) => this.SportSupplement = sportSupplement;
    public CreateCommand() {}

}
