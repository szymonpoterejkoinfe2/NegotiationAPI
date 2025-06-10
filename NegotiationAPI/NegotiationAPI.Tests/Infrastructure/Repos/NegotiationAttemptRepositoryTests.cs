using MapsterMapper;
using Moq;
using NegotiationAPI.Domain.Entities;
using NegotiationAPI.Domain.Enums;
using NegotiationAPI.Infrastructure.Persistance.Entities;
using NegotiationAPI.Infrastructure.Persistance.Repos;
using System.Reflection;

namespace NegotiationAPI.Tests;

[TestClass]
public class NegotiationAttemptRepositoryTests
{
    private Mock<IMapper> _mapperMock;
    private NegotiationAttemptRepository _repository;

    [TestInitialize]
    public void Setup()
    {
        _mapperMock = new Mock<IMapper>();

        // Reset the static list before every test
        var field = typeof(NegotiationAttemptRepository)
            .GetField("_negotiationAttemptEntities", BindingFlags.Static | BindingFlags.NonPublic);
        var list = field?.GetValue(null) as List<NegotiationAttemptEntity>;
        list?.Clear();

        _repository = new NegotiationAttemptRepository(_mapperMock.Object);
    }

    [TestMethod]
    public void AddNegotiationAttempt_ShouldAddAndReturnId()
    {
        var id = Guid.NewGuid();
        var attempt = new NegotiationAttempt
        {
            Id = id,
            ProposedAt = DateTime.UtcNow,
            ProposedPrice = 100,
            Result = NegotiationResult.AwaitingResponse
        };

        var attemptEntity = new NegotiationAttemptEntity
        {
            Id = id,
            ProposedAt = attempt.ProposedAt,
            ProposedPrice = attempt.ProposedPrice,
            Result = attempt.Result
        };

        _mapperMock.Setup(m => m.Map<NegotiationAttemptEntity>(attempt))
                   .Returns(attemptEntity);

        var result = _repository.AddNegotiationAttempt(attempt);

        Assert.AreEqual(id, result);
    }

    [TestMethod]
    public void DeleteNegotiationAttempt_ShouldRemove_WhenExists()
    {
        var id = Guid.NewGuid();
        var field = typeof(NegotiationAttemptRepository)
            .GetField("_negotiationAttemptEntities", BindingFlags.Static | BindingFlags.NonPublic);
        var list = field?.GetValue(null) as List<NegotiationAttemptEntity>;

        list?.Add(new NegotiationAttemptEntity
        {
            Id = id,
            ProposedPrice = 100,
            ProposedAt = DateTime.UtcNow,
            Result = NegotiationResult.AwaitingResponse
        });

        var result = _repository.DeleteNegotiationAttempt(id);

        Assert.IsTrue(result);
        Assert.IsFalse(list!.Any(a => a.Id == id));
    }

    [TestMethod]
    public void DeleteNegotiationAttempt_ShouldReturnFalse_WhenNotExists()
    {
        var result = _repository.DeleteNegotiationAttempt(Guid.NewGuid());
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void GetAttempts_ShouldReturnMappedList()
    {
        var entityList = new List<NegotiationAttemptEntity>
        {
            new() { Id = Guid.NewGuid(), ProposedPrice = 100, ProposedAt = DateTime.UtcNow, Result = NegotiationResult.Rejected }
        };

        var domainList = entityList.Select(e => new NegotiationAttempt
        {
            Id = e.Id,
            ProposedAt = e.ProposedAt,
            ProposedPrice = e.ProposedPrice,
            Result = e.Result
        });

        var field = typeof(NegotiationAttemptRepository)
            .GetField("_negotiationAttemptEntities", BindingFlags.Static | BindingFlags.NonPublic);
        var list = field?.GetValue(null) as List<NegotiationAttemptEntity>;
        list?.AddRange(entityList);

        _mapperMock.Setup(m => m.Map<IEnumerable<NegotiationAttempt>>(It.IsAny<IEnumerable<NegotiationAttemptEntity>>()))
                   .Returns(domainList);

        var result = _repository.GetAttempts();

        Assert.AreEqual(1, result.Count());
    }

    [TestMethod]
    public void GetNegotiationAttemptById_ShouldReturnMapped_WhenExists()
    {
        var id = Guid.NewGuid();
        var entity = new NegotiationAttemptEntity
        {
            Id = id,
            ProposedAt = DateTime.UtcNow,
            ProposedPrice = 120,
            Result = NegotiationResult.AwaitingResponse
        };

        var field = typeof(NegotiationAttemptRepository)
            .GetField("_negotiationAttemptEntities", BindingFlags.Static | BindingFlags.NonPublic);
        var list = field?.GetValue(null) as List<NegotiationAttemptEntity>;
        list?.Add(entity);

        var domain = new NegotiationAttempt
        {
            Id = id,
            ProposedAt = entity.ProposedAt,
            ProposedPrice = entity.ProposedPrice,
            Result = entity.Result
        };

        _mapperMock.Setup(m => m.Map<NegotiationAttempt>(entity))
                   .Returns(domain);

        var result = _repository.GetNegotiationAttemptById(id);

        Assert.IsNotNull(result);
        Assert.AreEqual(id, result.Id);
    }

    [TestMethod]
    public void UpdateNegotiationAttemptResultState_ShouldChangeResult()
    {
        var id = Guid.NewGuid();
        var field = typeof(NegotiationAttemptRepository)
            .GetField("_negotiationAttemptEntities", BindingFlags.Static | BindingFlags.NonPublic);
        var list = field?.GetValue(null) as List<NegotiationAttemptEntity>;

        list?.Add(new NegotiationAttemptEntity
        {
            Id = id,
            ProposedAt = DateTime.UtcNow,
            ProposedPrice = 130,
            Result = NegotiationResult.AwaitingResponse
        });

        _mapperMock.Setup(m => m.Map<NegotiationAttempt>(It.IsAny<NegotiationAttemptEntity>()))
                   .Returns((NegotiationAttemptEntity entity) => new NegotiationAttempt
                   {
                       Id = entity.Id,
                       ProposedAt = entity.ProposedAt,
                       ProposedPrice = entity.ProposedPrice,
                       Result = entity.Result
                   });

        var updated = _repository.UpdateNegotiationAttemptResultState(id, NegotiationResult.Rejected);

        Assert.IsNotNull(updated);
        Assert.AreEqual(NegotiationResult.Rejected, updated.Result);
    }
}