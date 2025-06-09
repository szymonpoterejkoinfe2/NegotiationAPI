using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NegotiationAPI.Application.CQRS.Commands.NegotiationAttempt.AcceptAttemptCommand;
using NegotiationAPI.Application.CQRS.Commands.NegotiationAttempt.DeleteAttemptCommand;
using NegotiationAPI.Application.CQRS.Commands.NegotiationAttempt.MakeAttemptCommand;
using NegotiationAPI.Application.CQRS.Commands.NegotiationAttempt.RejectAttemptCommand;
using NegotiationAPI.Application.CQRS.Queries.NegotiationAttempt.GetAllAttemptsQuery;
using NegotiationAPI.Application.CQRS.Queries.NegotiationAttempt.GetAttemptByIdQuery;
using NegotiationAPI.Application.CQRS.Queries.NegotiationAttempt.GetPendingAttemptsQuery;
using NegotiationAPI.Contracts.NegotiationAttempt;


namespace NegotiationAPI.Api.Controllers
{
    [Route("[controller]/")]
    public class NegotiationAttemptController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public NegotiationAttemptController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet("Waiting")]
        public async Task<IActionResult> GetAllPendingNegotiations()
        {
            var query = new GetPendingAttemptsQuery();
            var queryResult = await _mediator.Send(query);

            return queryResult.Match(
               queryResult => Ok(queryResult),
               errors => Problem(errors)
                );
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNegotiations()
        {
            var query = new GetAllAttemptsQuery();
            var queryResult = await _mediator.Send(query);

            return queryResult.Match(
               queryResult => Ok(queryResult),
               errors => Problem(errors)
                );
        }

        [HttpGet("{attemptId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetNegotiationAttemptById(Guid attemptId)
        {
            var query = new GetAttemptByIdQuery(AttemptId: attemptId);
            var queryResult = await _mediator.Send(query);

            return queryResult.Match(
               queryResult => Ok(queryResult),
               errors => Problem(errors)
                );
        }


        [HttpDelete("{attemptId}")]
        public async Task<IActionResult> DeleteNegotiationAttempt(Guid attemptId)
        {
            var command = new DeleteAttemptCommand(AttemptId: attemptId);
            var commandResult = await _mediator.Send(command);

            return commandResult.Match(
               commandResult => NoContent(),
               errors => Problem(errors)
                );
        }


        [HttpPost]
        public async Task<IActionResult> MakeAttempt([FromBody] MakeAttemptRequest request)
        {
            var command = new MakeAttemptCommand(NegotiationId: request.NegotiationId, ProposedPrice: request.ProposedPrice);
            var commandResult = await _mediator.Send(command);

            return commandResult.Match(
               commandResult => Ok(_mapper.Map<MakeAttemptResponse>(commandResult)),
               errors => Problem(errors)
                );
        }

        [HttpPut("accept/{attemptId}")]
        public async Task<IActionResult> AcceptAttempt(Guid attemptId)
        {
            var command = new AcceptAttemptCommand(AttemptId: attemptId);

            var commandResult = await _mediator.Send(command);

            return commandResult.Match(
               commandResult => Ok($"Attempt: {attemptId} accepted!"),
               errors => Problem(errors)
                );
        }


        [HttpPut("reject/{attemptId}")]
        public async Task<IActionResult> RejectAttempt(Guid attemptId)
        {
            var command = new RejectAttemptCommand(AttemptId: attemptId);
            var commandResult = await _mediator.Send(command);

            return commandResult.Match(
               commandResult => Ok($"Attempt: {attemptId} rejected!"),
               errors => Problem(errors)
                );
        }
    }
}
