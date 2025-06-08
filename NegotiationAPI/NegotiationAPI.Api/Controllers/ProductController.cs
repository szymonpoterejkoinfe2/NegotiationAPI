using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NegotiationAPI.Application.Authentication.Queries.Product.GetAllProductsQuery;
using NegotiationAPI.Application.Authentication.Queries.Product.GetProductByIdQuery;
using NegotiationAPI.Application.CQRS.Commands.Product.AddProductCommand;
using NegotiationAPI.Application.CQRS.Commands.Product.DeleteProductCommand;
using NegotiationAPI.Contracts.Product;
using NegotiationAPI.Domain.Entities;

namespace NegotiationAPI.Api.Controllers
{
    [Route("[controller]/")]
    public class ProductController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public ProductController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        [AllowAnonymous]
        [Route("GetAllProducts")]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var query = new GetAllProductsQuery();
            ErrorOr<List<Product>> queryResult = await _mediator.Send(query);

            return queryResult.Match(
               queryResult => Ok(_mapper.Map<List<ProductResponse>>(queryResult)),
               errors => Problem(errors)
               ); 

        }
        [AllowAnonymous]
        [Route("GetProdyctById")]
        [HttpGet]
        public async Task<IActionResult> GetProductById(Guid productId)
        {
            var query = new GetProductByIdQuery(Id: productId);
            ErrorOr<Product> queryResult = await _mediator.Send(query);

            return queryResult.Match(
               queryResult => Ok(_mapper.Map<ProductResponse>(queryResult)),
               errors => Problem(errors)
               ); 
        }

        [Route("AddProduct")]
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] AddProductRequest request)
        {
            var command = _mapper.Map<AddProductCommand>(request);
            ErrorOr<Success> commandResult = await _mediator.Send(command);

            return commandResult.Match(
            queryResult => Ok(queryResult),
            errors => Problem(errors)
            ); 
        }

        [Route("DeleteProduct")]
        [HttpDelete]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            var command = new DeleteProductCommand(Id: productId);
            ErrorOr<Success> commandResult = await _mediator.Send(command);

            return commandResult.Match(
            queryResult => Ok(queryResult),
            errors => Problem(errors)
            );
        }
    }
}
