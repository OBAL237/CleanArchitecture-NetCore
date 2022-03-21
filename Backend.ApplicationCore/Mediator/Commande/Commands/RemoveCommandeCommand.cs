using Backend.Models;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Mediator
{
    public class RemoveCommandeCommand: IRequest<int>
    {
        public Guid Id { get; set; }
    }
}
