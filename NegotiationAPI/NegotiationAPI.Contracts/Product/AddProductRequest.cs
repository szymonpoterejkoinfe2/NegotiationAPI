namespace NegotiationAPI.Contracts.Product
{
    public record AddProductRequest
    (
      string Name,
      double Price,
      string Description
    );

}
