
using MediatR;

using SportSupplements_API.Core.Repositories;
using SportSupplements_API.Infrastructure.Commands;


namespace SportSupplements_API.Infrastructure.Handlers;

public class UpdateHandler : IRequestHandler<UpdateCommand>
{
    private readonly ISportSupplementRepository sportSupplementRepository;

    public UpdateHandler(ISportSupplementRepository sportSupplementRepository) => this.sportSupplementRepository = sportSupplementRepository;

    public async Task Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request.Id);

        ArgumentNullException.ThrowIfNull(request.SportSupplement);

        await this.sportSupplementRepository.UpdateAsync((int)request.Id, request.SportSupplement);
    }

}
