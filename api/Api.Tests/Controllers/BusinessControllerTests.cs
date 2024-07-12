using Api.Controllers;
using Api.Controllers.Request;
using Api.Data;
using Api.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;

namespace Api.Tests.Controllers;

public class BusinessControllerTests
{

    private Mock<DbSet<Business>> BusinessSet;
    private readonly Mock<ApiDbContext> ApiDbContext;
    private readonly BusinessController Subject;

    public BusinessControllerTests()
    {
        BusinessSet = new Mock<DbSet<Business>>();
        ApiDbContext = new Mock<ApiDbContext>();
        ApiDbContext.Setup(c => c.Businesses).Returns(BusinessSet.Object);

        Subject = new BusinessController(ApiDbContext.Object);
    }
    
    
    [Fact]
    public  void Post_CreatesAndReturnsNewBusiness()
    {
        var testName = "Dan Flash's";
        var request = new CreateBusinessRequest(testName);

        var response = Subject.Post(request);

        // Validate Response
        var result = (ObjectResult?) response.Result;
        result.ShouldNotBeNull();
        result.StatusCode.ShouldNotBeNull();
        int statusCode = (int)result.StatusCode;
        statusCode.ShouldBe(StatusCodes.Status200OK);
        result.Value.ShouldBeOfType(typeof(Business));
        Business resultBusiness = (Business)result.Value;
        resultBusiness.Name.ShouldBe(testName);

        // Validate Calls
        BusinessSet.Verify(x => x.Add(It.Is<Business>(b => b.Name == testName)), Times.Once());
        ApiDbContext.Verify(x => x.SaveChanges());
    }
}