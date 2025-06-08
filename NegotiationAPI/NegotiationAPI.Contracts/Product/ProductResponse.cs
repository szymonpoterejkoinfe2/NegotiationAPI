namespace NegotiationAPI.Contracts.Product
{
    public record ProductResponse
    (
      Guid Id,
      string Name,
      double Price,
      string Description
    );
}
