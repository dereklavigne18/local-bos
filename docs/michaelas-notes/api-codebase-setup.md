# Api Codebase Setup

This doc will cover how the Api and Api.Tests projects were setup. This can be used as a reference for creation of future projects.

## Creating the Api Project

1. Create the project itself. This does the bulk of the work for you.
    ```
    cd api
    dotnet new webapi Api
    ```
2. Enable discoverable [Api Controllers](api/Api/Controllers). In [Program.cs](api/Api/Program.cs) configure the app builder to find the controllers with `builder.Services.AddControllers();` and provide a default action route through `app.MapControllerRoute(name: "default", pattern: "{controller=HelloWorld}/{action=Index}");`.
3. Create a new Controller in the [Api Controllers](api/Api/Controllers) directory. Make sure it has the right Api attributes. See the following as an example.
    ```
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
    ```

## Creating the Api.Tests Project

1. Create the project
    ```
    cd api
    dotnet new xunit Api.Tests
    ```
2. Add the Project Under Test to the Test project
    ```
    cd api/Api.Tests
    dotnet add reference ../Api/Api.csproj
    ```
3. Write some Xunit tests in the new project. See the following as a minimal example.
    ```
    namespace Api.Tests;

    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.True(true);
        }
    }
    ```
4. Run the tests to make sure they work
    ```
    cd api/Api.Tests
    dotnet test
    ```

## Creating the Solution

1. Create the solution
    ```
    cd api
    dotnet new sln --name api
    ```
2. Add the projects to the solution
    ```
    cd api
    dotnet sln add Api
    dotnet sln add Api.Tests
    ```