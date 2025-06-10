using MapsterMapper;
using Moq;
using NegotiationAPI.Domain.Entities;
using NegotiationAPI.Infrastructure.Persistance.Entities;
using NegotiationAPI.Infrastructure.Persistance.Repos;
using System.Reflection;

namespace NegotiationAPI.Tests;


[TestClass]
public class EmployeeRepositoryTests
{
    private Mock<IMapper> _mapperMock;
    private EmployeeRepository _repository;

    [TestInitialize]
    public void Setup()
    {
        _mapperMock = new Mock<IMapper>();
        _repository = new EmployeeRepository(_mapperMock.Object);

        // Uzyskaj dostêp do pola _employees
        var field = typeof(EmployeeRepository)
            .GetField("_employees", BindingFlags.Static | BindingFlags.NonPublic);

        // Pobierz istniej¹c¹ listê i j¹ wyczyœæ
        var list = field?.GetValue(null) as List<EmployeeEntity>;
        list?.Clear();
    }


    [TestMethod]
    public void Add_ShouldMapAndAddEmployee()
    {
        // Arrange
        var employee = new Employee { Email = "john@example.com" };
        var employeeEntity = new EmployeeEntity { Email = "john@example.com" };

        _mapperMock.Setup(m => m.Map<EmployeeEntity>(employee)).Returns(employeeEntity);
        _mapperMock.Setup(m => m.Map<Employee>(employeeEntity)).Returns(employee);

        // Act
        _repository.Add(employee);
        var result = _repository.GetEmployeeByEmail("john@example.com");

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("john@example.com", result.Email);
    }

    [TestMethod]
    public void GetEmployeeByEmail_ShouldReturnNull_WhenNotFound()
    {
        // Act
        var result = _repository.GetEmployeeByEmail("notfound@example.com");

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod]
    public void GetRandomEmployee_ShouldReturnEmployee_WhenExists()
    {
        // Arrange
        var employee = new Employee { Email = "jane@example.com" };
        var entity = new EmployeeEntity { Email = "jane@example.com" };

        _mapperMock.Setup(m => m.Map<EmployeeEntity>(employee)).Returns(entity);
        _mapperMock.Setup(m => m.Map<Employee>(entity)).Returns(employee);

        _repository.Add(employee);

        // Act
        var result = _repository.GetRandomEmployee();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("jane@example.com", result.Email);
    }

    [TestMethod]
    public void GetRandomEmployee_ShouldReturnNull_WhenEmpty()
    {
        // Act
        var result = _repository.GetRandomEmployee();

        // Assert
        Assert.IsNull(result);
    }
}
