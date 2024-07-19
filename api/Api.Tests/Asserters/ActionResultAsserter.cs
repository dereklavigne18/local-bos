using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shouldly;

namespace Api.Tests.Asserters;

class ActionResultAsserter
{

    public static T AssertIsOk<T>(ActionResult<T> actionResult, Action<T>? validateT = null)
    {
        var result = (ObjectResult?) actionResult.Result;
        result.ShouldNotBeNull();
        result.StatusCode.ShouldNotBeNull();
        int statusCode = (int)result.StatusCode;
        statusCode.ShouldBe(StatusCodes.Status200OK);
        result.Value.ShouldNotBeNull();
        result.Value.ShouldBeAssignableTo<T>();
        T resultData = (T)result.Value;
        
        if (validateT is not null)
        {
            validateT(resultData);
        }
    
        return resultData;
    }
}