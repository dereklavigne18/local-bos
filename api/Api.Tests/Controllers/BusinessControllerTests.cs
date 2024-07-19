using Api.Controllers;
using Api.Controllers.Request;
using Api.Data;
using Api.Entity;
using Api.Tests.Asserters;
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

    private void StageBusinesses(IEnumerable<Business> businesses)
    {
        foreach (var business in businesses)
        {
            ApiDbContext.Businesses.Add(business);
        }

        ApiDbContext.SaveChanges();
    }

    [Fact]
    public void Get_ReturnsAllBusinesses()
    {
        var danFlashs = new Business(Guid.NewGuid(), "Dan Flash's");
        var truffonis = new Business(Guid.NewGuid(), "Truffonis");
        StageBusinesses([danFlashs, truffonis]);

        var response = Subject.Get();

        // Validate Response
        ActionResultAsserter.AssertIsOk(response,
            (businesses) => businesses.ToList().ShouldBe(new List<Business> { danFlashs, truffonis }));
    }
    
    [Fact]
    public void Post_CreatesAndReturnsNewBusiness()
    {
        var testName = "Dan Flash's";
        var request = new WriteBusinessRequest(testName);

        var response = Subject.Post(request);

        // Validate Response
        ActionResultAsserter.AssertIsOk(
            response, (b) => b.Name.ShouldBe(testName));

        // Validate Calls
        var savedBusiness = ApiDbContext.Businesses.Where(b => b.Name == testName).FirstOrDefault();
        savedBusiness.ShouldNotBeNull();
    }

    [Fact]
    public void Put_UpdatesAndReturnsBusiness()
    {
        var danFlashsId = Guid.NewGuid();
        var danFlashs = new Business(danFlashsId, "Dan Flash's");
        StageBusinesses([danFlashs]);

        var truffonisName = "Truffonis";
        var request = new WriteBusinessRequest(truffonisName);
        var response = Subject.Put(danFlashsId, request);

        // Validate Response
        ActionResultAsserter.AssertIsOk(response,
            (b) => {
                b.Id.ShouldBe(danFlashsId);
                b.Name.ShouldBe(truffonisName);
            });

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
        StageBusinesses([danFlashs]);

        var response = Subject.Delete(id);

        // Validate Response
        ActionResultAsserter.AssertIsOk(response,
            (b) => {
                b.Id.ShouldBe(id);
                b.Name.ShouldBe(name);
            });

        // Validate Calls
        var savedBusiness = ApiDbContext.Businesses.Where(b =>
            b.Id == id && b.Name == name)
            .FirstOrDefault();
        savedBusiness.ShouldBeNull();
    }
}