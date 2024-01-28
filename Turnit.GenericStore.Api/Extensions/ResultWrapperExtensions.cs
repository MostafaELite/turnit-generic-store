using Microsoft.AspNetCore.Mvc;
using System.Net;
using TurnitStore.Domain.Models;

namespace Turnit.GenericStore.Api.Extensions
{
    public static class ResultWrapperExtensions
    {
        public static IActionResult ToApiResponse<TResult>(this ResultWrapper<TResult> result)
        {
            return result.Status switch
            {
                ResultStatus.Success => new OkObjectResult(result.Result),
                ResultStatus.NotFound => new NotFoundObjectResult(result.Message),
                ResultStatus.ShouldNotComplete => new BadRequestObjectResult(result.Message),
                _ => new ObjectResult(result.Message) { StatusCode = (int)HttpStatusCode.InternalServerError },
            };
        }
    }
}
