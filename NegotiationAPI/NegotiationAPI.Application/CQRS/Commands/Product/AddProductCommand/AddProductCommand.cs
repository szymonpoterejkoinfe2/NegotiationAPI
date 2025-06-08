using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationAPI.Application.CQRS.Commands.Product.AddProductCommand
{
    public record AddProductCommand 
    (
        string Name,
        double Price,
        string Description 
    ) : IRequest<ErrorOr<Success>>;
}
