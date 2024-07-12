using System.Net.Mime;
using Api.Controllers.Request;
using Api.Data;
using Api.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public class BusinessController(ApiDbContext dbContext) : Controller
{
    private ApiDbContext DbContext = dbContext;

    [HttpGet]
    [Route("business")]
    [ProducesResponseType<IEnumerable<Business>>(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Business>> Get()
    {
        return Ok(DbContext.Businesses);
    }

    [HttpPost]
    [Route("business")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType<Business>(StatusCodes.Status200OK)]
    public ActionResult<Business> Post([FromBody] CreateBusinessRequest request)
    {
        var business = new Business(Guid.NewGuid(), request.Name);

        DbContext.Businesses.Add(business);
        DbContext.SaveChanges();

        return Ok(business);
    }
}