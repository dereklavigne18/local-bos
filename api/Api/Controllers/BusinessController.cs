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
    public ActionResult<Business> Post([FromBody] WriteBusinessRequest request)
    {
        var business = new Business(Guid.NewGuid(), request.Name);

        DbContext.Businesses.Add(business);
        DbContext.SaveChanges();

        return Ok(business);
    }

    [HttpPut]
    [Route("business/{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType<Business>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Business> Put([FromRoute] Guid id, [FromBody] WriteBusinessRequest request)
    {
        var business = DbContext.Businesses.Where(b => b.Id == id).FirstOrDefault();
        if (business is null)
        {
            return NotFound();
        }

        business.Name = request.Name;
        DbContext.SaveChanges();

        return Ok(business);
    }

    [HttpDelete]
    [Route("business/{id}")]
    [ProducesResponseType<Business>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Business> Delete([FromRoute] Guid id)
    {
        var business = DbContext.Businesses.Where(b => b.Id == id).FirstOrDefault();
        if (business is null)
        {
            return NotFound();
        }

        DbContext.Businesses.Remove(business);
        DbContext.SaveChanges();

        return Ok(business);
    }
}