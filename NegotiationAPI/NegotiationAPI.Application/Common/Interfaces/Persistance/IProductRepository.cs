using NegotiationAPI.Domain.Entities;

namespace NegotiationAPI.Application.Common.Interfaces.Persistance
{
    public interface IProductRepository
    {
       Product? GetProductById(Guid productId);
       
       IEnumerable<Product> GetAllProducts();

       void AddProduct(Product product);

       bool DeleteProduct(Product product);
    }
}
