using MapsterMapper;
using Moq;
using NegotiationAPI.Domain.Entities;
using NegotiationAPI.Infrastructure.Persistance.Entities;
using NegotiationAPI.Infrastructure.Persistance.Repos;
using System.Reflection;

namespace NegotiationAPI.Tests;

[TestClass]
public class ProductRepositoryTests
{
    private Mock<IMapper> _mapperMock;
    private ProductRepository _repository;

    [TestInitialize]
    public void Setup()
    {
        _mapperMock = new Mock<IMapper>();

        var field = typeof(ProductRepository)
            .GetField("_productEntities", BindingFlags.Static | BindingFlags.NonPublic);
        var list = field?.GetValue(null) as List<ProductEntity>;
        list?.Clear();

        list?.AddRange(new List<ProductEntity>
            {
                new ProductEntity
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Laptop Lenovo ThinkPad X1",
                    Price = 5499.99,
                    Description = "Biznesowy laptop z procesorem Intel i7, 16GB RAM i dyskiem SSD 512GB."
                },
                new ProductEntity
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "Smartfon Samsung Galaxy S24",
                    Price = 3899.00,
                    Description = "Nowoczesny smartfon z ekranem AMOLED 6.8\" i aparatem 108MP."
                }
            });

        _repository = new ProductRepository(_mapperMock.Object);

        _mapperMock.Setup(m => m.Map<ProductEntity>(It.IsAny<Product>()))
            .Returns<Product>(p => new ProductEntity
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description
            });

        _mapperMock.Setup(m => m.Map<Product>(It.IsAny<ProductEntity>()))
            .Returns<ProductEntity>(pe => new Product
            {
                Id = pe.Id,
                Name = pe.Name,
                Price = pe.Price,
                Description = pe.Description
            });

        _mapperMock.Setup(m => m.Map<IEnumerable<Product>>(It.IsAny<IEnumerable<ProductEntity>>()))
            .Returns<IEnumerable<ProductEntity>>(peList => peList.Select(pe => new Product
            {
                Id = pe.Id,
                Name = pe.Name,
                Price = pe.Price,
                Description = pe.Description
            }));
    }

    [TestMethod]
    public void AddProduct_ShouldAddNewProduct()
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Nowy produkt",
            Price = 100,
            Description = "Opis produktu"
        };

        _repository.AddProduct(product);

        var all = _repository.GetAllProducts();

        Assert.IsTrue(all.Any(p => p.Id == product.Id && p.Name == "Nowy produkt"));
    }

    [TestMethod]
    public void DeleteProduct_ShouldReturnTrue_WhenProductExists()
    {
        var productToDelete = new Product
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Name = "Laptop Lenovo ThinkPad X1"
        };

        var result = _repository.DeleteProduct(productToDelete);

        Assert.IsTrue(result);

        var all = _repository.GetAllProducts();
        Assert.IsFalse(all.Any(p => p.Id == productToDelete.Id));
    }

    [TestMethod]
    public void DeleteProduct_ShouldReturnFalse_WhenProductDoesNotExist()
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Nieistniej¹cy produkt"
        };

        var result = _repository.DeleteProduct(product);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void GetAllProducts_ShouldReturnAllProducts()
    {
        var products = _repository.GetAllProducts();

        Assert.IsNotNull(products);
        Assert.AreEqual(2, products.Count()); 
    }

    [TestMethod]
    public void GetProductById_ShouldReturnProduct_WhenExists()
    {
        var id = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var product = _repository.GetProductById(id);

        Assert.IsNotNull(product);
        Assert.AreEqual("Laptop Lenovo ThinkPad X1", product.Name);
    }

    [TestMethod]
    public void GetProductById_ShouldReturnNull_WhenNotExists()
    {
        var product = _repository.GetProductById(Guid.NewGuid());

        Assert.IsNull(product);
    }
}
