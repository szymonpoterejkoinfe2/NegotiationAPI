using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationAPI.Application.CQRS.Commands.Product.DeleteProductCommand
{
    public record DeleteProductCommand
    (
        Guid Id
    ) : IRequest<ErrorOr<Success>>;
}
