using Backend.Models;
using MediatR;

namespace Mediator
{
    public class AddTypeProprieteCommand: TypeProprieteDto, IRequest<int>
    {

    }
}
