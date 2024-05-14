using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SportSupplements_API.Core.Models;

namespace SportSupplements_API.Infrastructure.Commands;

public class UpdateCommand : IRequest
{
    public int? Id { get; set; }
    public SportSupplement? SportSupplement { get; set; }

    public UpdateCommand(int? id, SportSupplement? sportSupplement)
    {
        this.Id = id;

        this.SportSupplement = sportSupplement;
    }

    public UpdateCommand() { }

}
