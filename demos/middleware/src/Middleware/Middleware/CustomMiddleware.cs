using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.Middleware
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly string _name;

        public CustomMiddleware(RequestDelegate next, ILogger<CustomMiddleware> logger, string name)
        {
            _next = next;
            _logger = logger;
            _name = name;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation($"{_name} middleware handling request: {context.Request.Path}");
            //await context.Response.WriteAsync($"<p>{_name} Middleware!</p>");
            await _next.Invoke(context);
            _logger.LogInformation($"{_name} middleware finished handling request.");
        }
    }
}
