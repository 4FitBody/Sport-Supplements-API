using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SportSupplements_API.Core.Repositories;
using SportSupplements_API.Infrastructure.Commands;

namespace SportSupplements_API.Infrastructure.Handlers;

public class DeleteHandler : IRequestHandler<DeleteCommand>
{
    private readonly ISportSupplementRepository sportSupplementRepository;

    public DeleteHandler(ISportSupplementRepository sportSupplementRepository) => this.sportSupplementRepository = sportSupplementRepository;

    public async Task Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request.Id);

        await this.sportSupplementRepository.DeleteAsync((int)request.Id);
    }

}
