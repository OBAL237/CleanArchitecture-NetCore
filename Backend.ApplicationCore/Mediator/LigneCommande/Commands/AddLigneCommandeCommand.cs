using Backend.Models;
using MediatR;

namespace Mediator
{
    public class AddLigneCommandeCommand: LigneCommandeDto, IRequest<int>
    {

    }
}
