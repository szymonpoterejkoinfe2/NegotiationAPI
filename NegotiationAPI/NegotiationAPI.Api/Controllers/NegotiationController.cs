using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NegotiationAPI.Application.CQRS.Commands.Negotiation.AddNegotiationCommand;
using NegotiationAPI.Application.CQRS.Commands.Negotiation.ChangeNegotiationStatusCommand;
using NegotiationAPI.Application.CQRS.Commands.Negotiation.DeleteNegotiationCommand;
using NegotiationAPI.Application.CQRS.Queries.Negotiation.GetAllNegotiationsQuery;
using NegotiationAPI.Application.CQRS.Queries.Negotiation.GetNegotiationById;
using NegotiationAPI.Contracts.Negotiation;
using NegotiationAPI.Domain.Entities;

namespace NegotiationAPI.Api.Controllers
{
    [Route("[controller]/")]
    public class NegotiationController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        public NegotiationController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNegotiations()
        {
            var query = new GetAllNegotiationsQuery();
            ErrorOr<List<Negotiation>> queryResult = await _mediator.Send(query);

            return queryResult.Match(
               queryResult => Ok(queryResult),
               errors => Problem(errors)
                );
        }


        [HttpPost]
        [AllowAnonymous]
        public async  Task<IActionResult> AddNegotiation(Guid productId)
        {
            var command = new AddNegotiationCommand(ProductId: productId);
            ErrorOr<Negotiation> commandResult = await _mediator.Send(command);

            return commandResult.Match(
               commandResult => Ok(_mapper.Map<AddNegotiationResponse>(commandResult)),
               errors => Problem(errors)
               );

        }


        [HttpPut("{negotiationId}/status")]
        public async Task<IActionResult> ChangeNegotiationStatus(Guid negotiationId, [FromBody] ChangeNegotiationStateRequest request)
        {
            var command = new ChangeNegotiationStatusCommand(negotiationId, request.NegotiationStatus);
            ErrorOr<Negotiation> commandResult = await _mediator.Send(command);

            return commandResult.Match(
               commandResult => Ok(_mapper.Map<ChangeNegotiationStateResponse>(commandResult)),
               errors => Problem(errors)
               );
        }


        [HttpGet("{negotiationId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetNegotiationById(Guid negotiationId)
        {
            var query = new GetNegotiationByIdQuery(NegotiationId: negotiationId);
            ErrorOr<Negotiation> queryResult = await _mediator.Send(query);

            return queryResult.Match(
               queryResult => Ok(_mapper.Map<GetNegotiationByIdResponse>(queryResult)),
               errors => Problem(errors)
                );
        }


        [HttpDelete("{negotiationId}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteNegotiation(Guid negotiationId)
        {
            var command = new DeleteNegotiationCommand(NegotiationId: negotiationId);
            ErrorOr<Success> commandResult = await _mediator.Send(command);

            return commandResult.Match(
               commandResult => NoContent(),
               errors => Problem(errors)
               );
        }
    }
}
