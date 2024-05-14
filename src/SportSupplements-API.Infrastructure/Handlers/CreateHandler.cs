using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SportSupplements_API.Core.Repositories;
using SportSupplements_API.Infrastructure.Commands;

namespace SportSupplements_API.Infrastructure.Handlers;

public class CreateHandler : IRequestHandler<CreateCommand>
{
    private readonly ISportSupplementRepository sportSupplementRepository;

    public CreateHandler(ISportSupplementRepository sportSupplementRepository) => this.sportSupplementRepository = sportSupplementRepository;

    public async Task Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request.SportSupplement);

        await this.sportSupplementRepository.CreateAsync(request.SportSupplement);
    }

}
