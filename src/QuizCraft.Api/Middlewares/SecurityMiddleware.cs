// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

namespace QuizCraft.Api.Middlewares
{
    internal class SecurityMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public SecurityMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _configuration = config;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey("security-header") &&
                _configuration.GetValue<string>("SecurityToken") == context.Request.Headers["security-header"])
            {
                await _next.Invoke(context);
            }
            else
            {
                // Return an Unauthorized or Forbidden response
                context.Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
            }
        }
    }
}