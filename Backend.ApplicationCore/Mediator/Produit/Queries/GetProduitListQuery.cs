using Backend.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace Mediator
{
    public class GetProduitListQuery: IRequest<IEnumerable<ProduitResponse>>
    {

    }
}
