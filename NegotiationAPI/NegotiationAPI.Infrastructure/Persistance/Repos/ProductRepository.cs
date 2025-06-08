using MapsterMapper;
using NegotiationAPI.Application.Common.Interfaces.Persistance;
using NegotiationAPI.Domain.Entities;

namespace NegotiationAPI.Infrastructure.Persistance.Repos
{
    public class ProductRepository : IProductRepository
    {
        private static readonly List<ProductEntity> _productEntities = new List<ProductEntity>()
        {
            new ProductEntity
        {
            Id = Guid.NewGuid(),
            Name = "Laptop Lenovo ThinkPad X1",
            Price = 5499.99,
            Description = "Biznesowy laptop z procesorem Intel i7, 16GB RAM i dyskiem SSD 512GB."
        },
            new ProductEntity
        {
            Id = Guid.NewGuid(),
            Name = "Smartfon Samsung Galaxy S24",
            Price = 3899.00,
            Description = "Nowoczesny smartfon z ekranem AMOLED 6.8\" i aparatem 108MP."
        },
            new ProductEntity
        {
            Id = Guid.NewGuid(),
            Name = "Słuchawki Sony WH-1000XM5",
            Price = 1299.50,
            Description = "Bezprzewodowe słuchawki z aktywną redukcją szumów i długim czasem pracy na baterii."
        },
            new ProductEntity
        {
            Id = Guid.NewGuid(),
            Name = "Monitor Dell UltraSharp 27\"",
            Price = 1899.00,
            Description = "Wysokiej jakości monitor 4K dla grafików i profesjonalistów."
        },
            new ProductEntity
        {
            Id = Guid.NewGuid(),
            Name = "Klawiatura mechaniczna Logitech G Pro X",
            Price = 499.99,
            Description = "Klawiatura dla graczy z przełącznikami wymiennymi i podświetleniem RGB."
        }

        };


        private readonly IMapper _mapper;

        public ProductRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void AddProduct(Product product)
        {
            var productEntity = _mapper.Map<ProductEntity>(product);
            _productEntities.Add(productEntity);
        }

        public bool DeleteProduct(Product product)
        {
            var productToDelete = _mapper.Map<ProductEntity>(product);

            int index = _productEntities.FindIndex(p => p.Id == productToDelete.Id);

            if (index < 0)
            {
                return false;
            }

            _productEntities.RemoveAt(index);
        
            return true;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = _mapper.Map<IEnumerable<Product>>(_productEntities);

            return products ?? Enumerable.Empty<Product>();
        }

        public Product? GetProductById(Guid productId)
        {
            var product = _productEntities.Where(p => p.Id == productId).FirstOrDefault();

            return _mapper.Map<Product>(product);
        }
    }
}
