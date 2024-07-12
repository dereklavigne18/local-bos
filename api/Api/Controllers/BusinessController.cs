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

    [HttpPost]
    [Route("business/{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType<Business>(StatusCodes.Status200OK)]
    public IActionResult Write([FromRoute] Guid id, [FromBody] WriteBusinessRequest request)
    {
        var business = new Business(id, request.Name);

        DbContext.Businesses.Add(business);
        DbContext.SaveChanges();

        return Ok(business);
    }
}