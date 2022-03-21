using Backend.Models;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Mediator
{
    public class RemoveProprieteCommand: IRequest<int> 
    {
        public Guid Id { get; set; }
    }
}
