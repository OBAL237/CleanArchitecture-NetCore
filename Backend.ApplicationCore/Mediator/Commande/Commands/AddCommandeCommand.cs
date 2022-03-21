using Backend.Models;
using MediatR;

namespace Mediator
{
    public class AddCommandeCommand: CommandeDto, IRequest<int>
    {

    }
}
