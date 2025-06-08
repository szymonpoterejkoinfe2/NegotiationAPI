namespace NegotiationAPI.Contracts.Negotiation
{
    public record AddNegotiationResponse
    (
        Guid Id,
        string Status,
        DateTime CreatedAt
    );
}
