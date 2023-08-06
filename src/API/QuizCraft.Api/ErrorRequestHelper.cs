using Microsoft.AspNetCore.Mvc;
using OneOf;
using QuizCraft.Application.QuizManagement;
using System.Net;

namespace QuizCraft.Api
{
    public static class ErrorRequestHelper
    {
        public static ActionResult HandleError<T>(this OneOf<T, RequestError> result, ControllerBase controllerBase)
        {
            switch (result.AsT1.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return controllerBase.NotFound();
                case HttpStatusCode.UnprocessableEntity:
                    return controllerBase.UnprocessableEntity();
                case HttpStatusCode.BadRequest:
                    return controllerBase.BadRequest();
                default:
                    return controllerBase.BadRequest();
            }
        }
    }
}
