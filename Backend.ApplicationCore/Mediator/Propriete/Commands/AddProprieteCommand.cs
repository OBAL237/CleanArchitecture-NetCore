using Backend.Models;
using MediatR;

namespace Mediator
{
    public class AddProprieteCommand: ProprieteDto, IRequest<int>
    {

    }
}
