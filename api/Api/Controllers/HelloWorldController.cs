using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public class HelloWorldController : Controller
{
    [HttpGet]
    [Route("index")]
    public string Index()
    {
        return "Hello, World!";
    }
}