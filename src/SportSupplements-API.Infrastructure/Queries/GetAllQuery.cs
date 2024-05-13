
using MediatR;
using SportSupplements_API.Core.Models;

namespace SportSupplements_API.Infrastructure.Queries;

public class GetAllQuery : IRequest<IEnumerable<SportSupplement>>
{

}
