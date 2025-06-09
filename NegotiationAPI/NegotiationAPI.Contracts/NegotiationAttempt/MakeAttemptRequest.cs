namespace NegotiationAPI.Contracts.NegotiationAttempt
{
    public record MakeAttemptRequest
        (
            Guid NegotiationId,
            double ProposedPrice
        );
}
