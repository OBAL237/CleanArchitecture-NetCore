using Backend.Models;
using MediatR;

namespace Mediator
{
    public class AddProduitCommand: ProduitDto, IRequest<int>
    {

    }
}
