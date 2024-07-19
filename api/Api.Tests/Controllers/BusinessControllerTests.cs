using Api.Controllers;
using Api.Controllers.Request;
using Api.Data;
using Api.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Api.Tests.Controllers;

public class BusinessControllerTests
{

    private readonly ApiDbContext ApiDbContext;
    private readonly BusinessController Subject;

    public BusinessControllerTests()
    {
        var optionBuilder = new DbContextOptionsBuilder<ApiDbContext>()
            .UseInMemoryDatabase(databaseName: "local-bos");
        ApiDbContext = new ApiDbContext(optionBuilder.Options);
        ApiDbContext.Businesses.RemoveRange(ApiDbContext.Businesses);

        Subject = new BusinessController(ApiDbContext);
    }

    [Fact]
    public void Get_ReturnsAllBusinesses()
    {
        var danFlashs = new Business(Guid.NewGuid(), "Dan Flash's");
        var truffonis = new Business(Guid.NewGuid(), "Truffonis");
        ApiDbContext.Businesses.Add(danFlashs);
        ApiDbContext.Businesses.Add(truffonis);
        ApiDbContext.SaveChanges();

        var response = Subject.Get();

        // Validate Response
        var result = (ObjectResult?) response.Result;
        result.ShouldNotBeNull();
        result.StatusCode.ShouldNotBeNull();
        int statusCode = (int)result.StatusCode;
        statusCode.ShouldBe(StatusCodes.Status200OK);
        result.Value.ShouldNotBeNull();
        result.Value.ShouldBeAssignableTo<IEnumerable<Business>>();
        IEnumerable<Business> resultBusinesses = (IEnumerable<Business>)result.Value;
        resultBusinesses.ToList().ShouldBe(new List<Business> { danFlashs, truffonis });
    }
    
    [Fact]
    public void Post_CreatesAndReturnsNewBusiness()
    {
        var testName = "Dan Flash's";
        var request = new WriteBusinessRequest(testName);

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
        var savedBusiness = ApiDbContext.Businesses.Where(b => b.Name == testName).FirstOrDefault();
        savedBusiness.ShouldNotBeNull();
    }

    [Fact]
    public void Put_UpdatesAndReturnsBusiness()
    {
        var danFlashsId = Guid.NewGuid();
        var danFlashs = new Business(danFlashsId, "Dan Flash's");
        ApiDbContext.Businesses.Add(danFlashs);
        ApiDbContext.SaveChanges();

        var truffonisName = "Truffonis";
        var request = new WriteBusinessRequest(truffonisName);
        var response = Subject.Put(danFlashsId, request);

        // Validate Response
        var result = (ObjectResult?) response.Result;
        result.ShouldNotBeNull();
        result.StatusCode.ShouldNotBeNull();
        int statusCode = (int)result.StatusCode;
        statusCode.ShouldBe(StatusCodes.Status200OK);
        result.Value.ShouldBeOfType(typeof(Business));
        Business resultBusiness = (Business)result.Value;
        resultBusiness.Id.ShouldBe(danFlashsId);
        resultBusiness.Name.ShouldBe(truffonisName);

        // Validate Calls
        var savedBusiness = ApiDbContext.Businesses.Where(b =>
            b.Id == danFlashsId && b.Name == truffonisName)
            .FirstOrDefault();
        savedBusiness.ShouldNotBeNull();
    }

    [Fact]
    public void Delete_RemovesAndReturnsBusiness()
    {
        var id = Guid.NewGuid();
        var name = "Dan Flash's";
        var danFlashs = new Business(id, name);
        ApiDbContext.Businesses.Add(danFlashs);
        ApiDbContext.SaveChanges();

        var response = Subject.Delete(id);

        // Validate Response
        var result = (ObjectResult?) response.Result;
        result.ShouldNotBeNull();
        result.StatusCode.ShouldNotBeNull();
        int statusCode = (int)result.StatusCode;
        statusCode.ShouldBe(StatusCodes.Status200OK);
        result.Value.ShouldBeOfType(typeof(Business));
        Business resultBusiness = (Business)result.Value;
        resultBusiness.Id.ShouldBe(id);
        resultBusiness.Name.ShouldBe(name);

        // Validate Calls
        var savedBusiness = ApiDbContext.Businesses.Where(b =>
            b.Id == id && b.Name == name)
            .FirstOrDefault();
        savedBusiness.ShouldBeNull();
    }
}