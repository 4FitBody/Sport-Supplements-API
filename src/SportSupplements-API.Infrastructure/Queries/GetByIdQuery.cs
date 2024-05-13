using MediatR;
using SportSupplements_API.Core.Models;

namespace SportSupplements_API.Infrastructure.Queries;

public class GetByIdQuery : IRequest<SportSupplement>
{
    public int? Id { get; set; }

    public GetByIdQuery(int? id)
    {
        this.Id = id;
    }

    public GetByIdQuery() {}
}
