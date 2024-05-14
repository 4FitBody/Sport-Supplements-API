using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SportSupplements_API.Core.Models;
using SportSupplements_API.Core.Repositories;
using SportSupplements_API.Infrastructure.Queries;

namespace SportSupplements_API.Infrastructure.Handlers;


public class GetByIdHandler : IRequestHandler<GetByIdQuery, SportSupplement>
{
    private readonly ISportSupplementRepository sportSupplementRepository;

    public GetByIdHandler(ISportSupplementRepository sportSupplementRepository) => this.sportSupplementRepository = sportSupplementRepository;

    public async Task<SportSupplement> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request.Id);

        var sportSupplement = await this.sportSupplementRepository.GetByIdAsync((int)request.Id);

        if (sportSupplement is null)
        {
            return new SportSupplement();
        }

        return sportSupplement!;
    }
}
