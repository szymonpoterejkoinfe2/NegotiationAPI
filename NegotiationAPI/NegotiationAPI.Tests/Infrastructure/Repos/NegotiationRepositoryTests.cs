using MapsterMapper;
using Moq;
using NegotiationAPI.Domain.Entities;
using NegotiationAPI.Domain.Enums;
using NegotiationAPI.Infrastructure.Persistance.Repos;
using System.Reflection;

namespace NegotiationAPI.Tests;

[TestClass]
public class NegotiationRepositoryTests
{
    private Mock<IMapper> _mapperMock;
    private NegotiationRepository _repository;

    [TestInitialize]
    public void Setup()
    {
        _mapperMock = new Mock<IMapper>();

        var field = typeof(NegotiationRepository)
            .GetField("_negotiationEntities", BindingFlags.Static | BindingFlags.NonPublic);
        var list = field?.GetValue(null) as List<NegotiationAPI.Infrastructure.Persistance.Entities.NegotiationEntity>;
        list?.Clear();

        _repository = new NegotiationRepository(_mapperMock.Object);
    }

    [TestMethod]
    public void AddNegotiation_ShouldAddCorrectly()
    {
        var negotiation = new Negotiation
        {
            Id = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            Status = NegotiationStatus.Waiting,
            CreatedAt = DateTime.UtcNow,
            Attempts = new List<NegotiationAttempt>()
        };

        var entity = new NegotiationAPI.Infrastructure.Persistance.Entities.NegotiationEntity
        {
            Id = negotiation.Id,
            ProductId = negotiation.ProductId,
            Status = negotiation.Status,
            CreatedAt = negotiation.CreatedAt,
            Attempts = new List<NegotiationAPI.Infrastructure.Persistance.Entities.NegotiationAttemptEntity>()
        };

        _mapperMock.Setup(m => m.Map<NegotiationAPI.Infrastructure.Persistance.Entities.NegotiationEntity>(It.IsAny<Negotiation>()))
                   .Returns(entity);

        var result = _repository.AddNegotiation(negotiation);

        Assert.AreEqual(negotiation.Id, result);
    }

    [TestMethod]
    public void GetAllNegotiations_ShouldReturnMappedList()
    {
        var entityList = new List<NegotiationAPI.Infrastructure.Persistance.Entities.NegotiationEntity>
        {
            new() { Id = Guid.NewGuid(), ProductId = Guid.NewGuid(), Status = NegotiationStatus.Waiting, CreatedAt = DateTime.UtcNow, Attempts = new() }
        };

        var domainList = entityList.Select(e => new Negotiation { Id = e.Id, ProductId = e.ProductId, Status = e.Status, CreatedAt = e.CreatedAt, Attempts = new() });

        _mapperMock.Setup(m => m.Map<IEnumerable<Negotiation>>(It.IsAny<IEnumerable<NegotiationAPI.Infrastructure.Persistance.Entities.NegotiationEntity>>()))
                   .Returns(domainList);

        var field = typeof(NegotiationRepository)
            .GetField("_negotiationEntities", BindingFlags.Static | BindingFlags.NonPublic);
        var list = field?.GetValue(null) as List<NegotiationAPI.Infrastructure.Persistance.Entities.NegotiationEntity>;
        list?.AddRange(entityList);

        var results = _repository.GetAllNegotiations();

        Assert.IsNotNull(results);
        Assert.AreEqual(1, results.Count());
    }

    [TestMethod]
    public void DeleteNegotiation_ShouldRemove_WhenExists()
    {
        var id = Guid.NewGuid();
        var field = typeof(NegotiationRepository)
            .GetField("_negotiationEntities", BindingFlags.Static | BindingFlags.NonPublic);
        var list = field?.GetValue(null) as List<NegotiationAPI.Infrastructure.Persistance.Entities.NegotiationEntity>;

        list?.Add(new()
        {
            Id = id,
            ProductId = Guid.NewGuid(),
            Status = NegotiationStatus.Waiting,
            CreatedAt = DateTime.UtcNow,
            Attempts = new()
        });

        var result = _repository.DeleteNegotiation(id);

        Assert.IsTrue(result);
        Assert.IsFalse(list!.Any(n => n.Id == id));
    }

    [TestMethod]
    public void DeleteNegotiation_ShouldReturnFalse_WhenNotExists()
    {
        var result = _repository.DeleteNegotiation(Guid.NewGuid());
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void GetNegotiationById_ShouldReturnNegotiation_WhenExists()
    {
        var id = Guid.NewGuid();
        var entity = new NegotiationAPI.Infrastructure.Persistance.Entities.NegotiationEntity
        {
            Id = id,
            ProductId = Guid.NewGuid(),
            Status = NegotiationStatus.Waiting,
            CreatedAt = DateTime.UtcNow,
            Attempts = new()
        };

        var field = typeof(NegotiationRepository)
            .GetField("_negotiationEntities", BindingFlags.Static | BindingFlags.NonPublic);
        var list = field?.GetValue(null) as List<NegotiationAPI.Infrastructure.Persistance.Entities.NegotiationEntity>;
        list?.Add(entity);

        _mapperMock.Setup(m => m.Map<Negotiation>(It.IsAny<NegotiationAPI.Infrastructure.Persistance.Entities.NegotiationEntity>()))
                   .Returns(new Negotiation { Id = id });

        var result = _repository.GetNegotiationById(id);

        Assert.IsNotNull(result);
        Assert.AreEqual(id, result.Id);
    }

    [TestMethod]
    public void ChangeNegotiationStatus_ShouldUpdateStatus()
    {
        var id = Guid.NewGuid();
        var entity = new NegotiationAPI.Infrastructure.Persistance.Entities.NegotiationEntity
        {
            Id = id,
            ProductId = Guid.NewGuid(),
            Status = NegotiationStatus.Waiting,
            CreatedAt = DateTime.UtcNow,
            Attempts = new()
        };

        var field = typeof(NegotiationRepository)
            .GetField("_negotiationEntities", BindingFlags.Static | BindingFlags.NonPublic);
        var list = field?.GetValue(null) as List<NegotiationAPI.Infrastructure.Persistance.Entities.NegotiationEntity>;
        list?.Add(entity);

        _mapperMock.Setup(m => m.Map<Negotiation>(It.IsAny<NegotiationAPI.Infrastructure.Persistance.Entities.NegotiationEntity>()))
                   .Returns(new Negotiation { Id = id, Status = NegotiationStatus.Accepted });

        var result = _repository.ChangeNegotiationStatus(id, NegotiationStatus.Accepted);

        Assert.IsNotNull(result);
        Assert.AreEqual(NegotiationStatus.Accepted, result.Status);
    }

    [TestMethod]
    public void UpdateNegotiation_ShouldReplace_WhenExists()
    {
        var id = Guid.NewGuid();
        var field = typeof(NegotiationRepository)
            .GetField("_negotiationEntities", BindingFlags.Static | BindingFlags.NonPublic);
        var list = field?.GetValue(null) as List<NegotiationAPI.Infrastructure.Persistance.Entities.NegotiationEntity>;

        list?.Add(new()
        {
            Id = id,
            ProductId = Guid.NewGuid(),
            Status = NegotiationStatus.Waiting,
            CreatedAt = DateTime.UtcNow,
            Attempts = new()
        });

        var updated = new Negotiation
        {
            Id = id,
            ProductId = Guid.NewGuid(),
            Status = NegotiationStatus.Accepted,
            CreatedAt = DateTime.UtcNow,
            Attempts = new()
        };

        var updatedEntity = new NegotiationAPI.Infrastructure.Persistance.Entities.NegotiationEntity
        {
            Id = id,
            ProductId = updated.ProductId,
            Status = updated.Status,
            CreatedAt = updated.CreatedAt,
            Attempts = new()
        };

        _mapperMock.Setup(m => m.Map<NegotiationAPI.Infrastructure.Persistance.Entities.NegotiationEntity>(updated))
                   .Returns(updatedEntity);

        var result = _repository.UpdateNegotiation(updated);

        Assert.IsTrue(result);
        Assert.AreEqual(NegotiationStatus.Accepted, list!.First(n => n.Id == id).Status);
    }
}