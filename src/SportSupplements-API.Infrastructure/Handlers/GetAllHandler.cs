using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SportSupplements_API.Core.Models;
using SportSupplements_API.Core.Repositories;
using SportSupplements_API.Infrastructure.Queries;

namespace SportSupplements_API.Infrastructure.Handlers;

public class GetAllHandler : IRequestHandler<GetAllQuery, IEnumerable<SportSupplement>>
{
    private readonly ISportSupplementRepository sportSupplementRepository;

    public GetAllHandler(ISportSupplementRepository sportSupplementRepository) => this.sportSupplementRepository = sportSupplementRepository;

    public async Task<IEnumerable<SportSupplement>>? Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var sportSupplement = await this.sportSupplementRepository.GetAllAsync();

        if (sportSupplement is null)
        {
            return Enumerable.Empty<SportSupplement>();
        }

        return sportSupplement;
    }

}
