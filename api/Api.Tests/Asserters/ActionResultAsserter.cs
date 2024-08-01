using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shouldly;

namespace Api.Tests.Asserters;

class ActionResultAsserter
{
    public static void AssertIsOk<T>(ActionResult<T> actionResult, Action<T>? validateT = null)
    {
        var (statusCode, data) = UnpackResponse(actionResult);
        statusCode.ShouldBe(StatusCodes.Status200OK);

        if (validateT is not null)
        {
            data.ShouldNotBeNull();
            data.ShouldBeAssignableTo<T>();
            validateT((T)data);
        }
    }

    public static void AssertIsNotFound<T>(ActionResult<T> actionResult)
    {
        var statusCode = StatusCode(actionResult);

        statusCode.ShouldBe(StatusCodes.Status404NotFound);
    }

    private static int StatusCode<T>(ActionResult<T> actionResult)
    {
        var (statusCode, _) = UnpackResponse(actionResult);

        return statusCode;
    }

    private static (int, object?) UnpackResponse<T>(ActionResult<T> actionResult)
    {
        try
        {
            var objResult = (ObjectResult?)actionResult.Result;
            objResult.ShouldNotBeNull();
            objResult.StatusCode.ShouldNotBeNull();
            int statusCode = (int)objResult.StatusCode;

            return (statusCode, objResult.Value);
        }
        catch (InvalidCastException)
        {
            var statusResult = (StatusCodeResult?)actionResult.Result;
            statusResult.ShouldNotBeNull();

            return (statusResult.StatusCode, null);
        }
    }
}