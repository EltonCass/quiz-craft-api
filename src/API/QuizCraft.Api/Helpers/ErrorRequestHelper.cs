// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.AspNetCore.Mvc;
using OneOf;
using QuizCraft.Application.QuizManagement;
using System.Net;

namespace QuizCraft.Api.Helpers;

public static class RequestErrorHelper
{
    public static ActionResult HandleError<T>(this OneOf<T, RequestError> result, ControllerBase controllerBase)
    {
        return result.AsT1.StatusCode switch
        {
            HttpStatusCode.NotFound => controllerBase.NotFound(),
            HttpStatusCode.UnprocessableEntity => controllerBase.UnprocessableEntity(),
            HttpStatusCode.BadRequest => controllerBase.BadRequest(),
            _ => controllerBase.BadRequest(),
        };
    }
}
