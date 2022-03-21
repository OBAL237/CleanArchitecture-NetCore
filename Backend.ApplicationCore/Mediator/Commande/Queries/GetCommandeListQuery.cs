using Backend.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace Mediator
{
    public class GetCommandeListQuery: IRequest<IEnumerable<CommandeResponse>>
    {

    }
}
